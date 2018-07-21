using System;

namespace WinPartyArs.Common
{
    [Serializable]
    public class TesonetServerMessageException : Exception
    {
        public TesonetServerMessageException() { }
        public TesonetServerMessageException(string message) : base(message) { }
        public TesonetServerMessageException(string message, Exception inner) : base(message, inner) { }
        protected TesonetServerMessageException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class TesonetUnexepectedResponseException : Exception
    {
        public TesonetUnexepectedResponseException() { }
        public TesonetUnexepectedResponseException(string message) : base(message) { }
        public TesonetUnexepectedResponseException(string message, Exception inner) : base(message, inner) { }
        protected TesonetUnexepectedResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
