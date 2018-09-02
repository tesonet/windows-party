
namespace CaliburnMicro.LoginTestExternal.Framework
{
    using System;
    using System.IO;
    using Caliburn.Micro;


    public class SimpleFileLogger : ILog
    {
        #region Fields

        private readonly Type type;
        private string filename;

        #endregion Fields

        #region Constructors

      
        public SimpleFileLogger(Type type, Func<bool> canDeleteFile)
        {
            this.type = type;
            this.filename = string.Format("{0}.log", DateTime.Today.ToString("yyyy-MM-dd"));

         
            if (canDeleteFile() && File.Exists(this.filename))
            {
                File.Delete(this.filename);
            }
        }

        #endregion Constructors

        #region Methods

    
        public void Error(Exception exception)
        {
            this.WriteMessage(this.CreateLogMessage(exception.ToString()), "ERROR");
        }


        public void Info(string format, params object[] args)
        {
            this.WriteMessage(this.CreateLogMessage(format, args), "INFO");
        }


        public void Warn(string format, params object[] args)
        {
            this.WriteMessage(this.CreateLogMessage(format, args), "WARN");
        }

      
        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format(
                "[{0}] {1}",
                DateTime.Now.ToString("o"),
                string.Format(format, args));
        }

   
        private void WriteMessage(string message, string catgeory)
        {
            using (StreamWriter writer = new StreamWriter(this.filename, true))
            {
                writer.WriteLine("{0}: {1}", catgeory, message);
            }
        }

        #endregion Methods
    }
}