using Prism.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using WinPartyArs.Common;

namespace WinPartyArs.Models
{
    public class StreamWithSecurePassword : Stream
    {
        enum CurrentyReading { Prefix = 0, Password, Suffix }

        private ILoggerFacade _log;
        private Encoding _enc;
        private byte[] _prefixBytes;
        private SecureString _password;
        private byte[] _suffixBytes;
        private IntPtr _passwordPtr = IntPtr.Zero;
        private CurrentyReading _currentlyReading = CurrentyReading.Prefix;
        private Dictionary<char, byte[]> _escapedCharsToBytes;

        public StreamWithSecurePassword(ILoggerFacade log, string prefix, SecureString password, string suffix, Encoding enc,
            Dictionary<char, byte[]> escapedCharsToBytes)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _enc = enc ?? throw new ArgumentNullException(nameof(enc));
            _escapedCharsToBytes = escapedCharsToBytes;
            _prefixBytes = _enc.GetBytes(prefix ?? throw new ArgumentNullException(nameof(prefix)));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            _suffixBytes = _enc.GetBytes(suffix ?? throw new ArgumentNullException(nameof(suffix)));

            var maxEscapedCharLength = 0;
            if (null != _escapedCharsToBytes && null != _escapedCharsToBytes.Values)
                foreach (var escapedByteArray in _escapedCharsToBytes.Values)
                {
                    if (null == escapedByteArray)
                        throw new ArgumentException($"{nameof(escapedCharsToBytes)} contains byte array NULL!");
                    if (escapedByteArray.Length == 0)
                        throw new ArgumentException($"{nameof(escapedCharsToBytes)} contains 0 length byte array!");
                    if (escapedByteArray.Length > maxEscapedCharLength)
                        maxEscapedCharLength = escapedByteArray.Length;
                }

            _currentPasswordCharBytes = new byte[Math.Max(_enc.GetMaxByteCount(1), maxEscapedCharLength)];
            _log.Log($"StreamWithSecurePassword() ctor finished initializing with '{_enc}' encoding", Category.Debug);
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => throw new NotImplementedException();

        private int _currentPartPosition;
        public override long Position
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override void Flush() => throw new NotImplementedException();

        private (int currentPos, int totalLen) _leftOversOfCurrentPasswordChar;
        private char[] _currentPasswordChar = new char[1];
        private byte[] _currentPasswordCharBytes;
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (null == buffer)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0 || offset >= buffer.Length)
                throw new ArgumentException($"{nameof(offset)} cannot be {offset}, when buffer.Length = {buffer.Length}");
            if (count < 1 || count > buffer.Length - offset)
                throw new ArgumentException($"{nameof(count)} cannot be {count}, when buffer.Length = {buffer.Length} and offset = {offset}");

            _log.Log($"StreamWithSecurePassword Read() called buffer.Length = '{buffer.Length}', offset = {offset}, count = {count}", Category.Debug);
            int haveBytes;
            switch (_currentlyReading)
            {
                case CurrentyReading.Prefix:
                    haveBytes = ReadInternal(buffer, ref offset, count, _prefixBytes, ref _currentPartPosition);
                    break;
                case CurrentyReading.Suffix:
                    return ReadInternal(buffer, ref offset, count, _suffixBytes, ref _currentPartPosition);
                case CurrentyReading.Password:
                    if (IntPtr.Zero == _passwordPtr)
                        _passwordPtr = Marshal.SecureStringToGlobalAllocUnicode(_password);
                    haveBytes = 0;
                    if (_leftOversOfCurrentPasswordChar.currentPos != 0)
                    {
                        haveBytes = ReadInternal(buffer, ref offset, count, _currentPasswordCharBytes,
                            ref _leftOversOfCurrentPasswordChar.currentPos, _leftOversOfCurrentPasswordChar.totalLen);
                        if (_leftOversOfCurrentPasswordChar.currentPos >= _leftOversOfCurrentPasswordChar.totalLen)
                            _leftOversOfCurrentPasswordChar.currentPos = 0;
                        if (haveBytes >= count)
                            return count;
                    }
                    
                    while (_currentPartPosition < _password.Length && haveBytes < count)
                    {
                        _currentPasswordChar[0] = (char)Marshal.ReadInt16(_passwordPtr, _currentPartPosition++ * 2);
                        if (char.IsControl(_currentPasswordChar[0]))
                            throw new ArgumentException("StreamWithSecurePassword password containing control character!");

                        int len;
                        if (null != _escapedCharsToBytes && _escapedCharsToBytes.TryGetValue(_currentPasswordChar[0], out var escapedBytes))
                        {
                            len = escapedBytes.Length;
                            Array.Copy(escapedBytes, _currentPasswordCharBytes, escapedBytes.Length);
                        }
                        else
                        {
                            len = _enc.GetBytes(_currentPasswordChar, 0, 1, _currentPasswordCharBytes, 0);
                        }
                        _currentPasswordChar[0] = (char)0;

                        if (len + haveBytes <= count)
                        {
                            Array.Copy(_currentPasswordCharBytes, 0, buffer, offset, len);
                            offset += len;
                            haveBytes += len;
                        }
                        else
                        {
                            Array.Copy(_currentPasswordCharBytes, 0, buffer, offset, count - haveBytes);
                            _leftOversOfCurrentPasswordChar = (count - haveBytes, len);
                            return count;
                        }
                    }

                    if (haveBytes < count)
                        ClearPasswordLeftovers();
                    break;
                default:
                    throw new InvalidDataException($"Invalid StreamWithSecurePassword _currentlyReading state: {_currentlyReading}");
            }

            if (haveBytes < count)
            {
                _currentPartPosition = 0;
                _currentlyReading++;
                return haveBytes + Read(buffer, offset, count - haveBytes);
            }
            else
                return haveBytes;
        }

        private void ClearPasswordLeftovers()
        {
            Marshal.ZeroFreeGlobalAllocUnicode(_passwordPtr);
            _passwordPtr = IntPtr.Zero;
            for (int i = 0; i < _currentPasswordChar.Length; i++)
                _currentPasswordChar[i] = (char)0;
            for (int i = 0; i < _currentPasswordCharBytes.Length; i++)
                _currentPasswordCharBytes[i] = 0;
            _log.Log($"StreamWithSecurePassword ClearPasswordLeftovers() finished", Category.Debug);
        }

        private static int ReadInternal(byte[] buffer, ref int offset, int count, byte[] someBytes, ref int currentPartPosition)
            => ReadInternal(buffer, ref offset, count, someBytes, ref currentPartPosition, someBytes.Length);


        private static int ReadInternal(byte[] buffer, ref int offset, int count, byte[] someBytes, ref int currentPartPosition, int someBytesLen)
        {
            var haveBytes = count > someBytesLen - currentPartPosition ? someBytesLen - currentPartPosition : count;
            Array.Copy(someBytes, currentPartPosition, buffer, offset, haveBytes);
            currentPartPosition += haveBytes;
            offset += haveBytes;
            return haveBytes;
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotImplementedException();

        public override void SetLength(long value) => throw new NotImplementedException();

        public override void Write(byte[] buffer, int offset, int count) => throw new NotImplementedException();

        public override void Close()
        {
            base.Close();
            if (IntPtr.Zero != _passwordPtr)
                ClearPasswordLeftovers();
            _log.Log($"StreamWithSecurePassword Close() finished", Category.Debug);
        }
    }
}
