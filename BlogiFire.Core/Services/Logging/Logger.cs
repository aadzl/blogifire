using System;

namespace BlogiFire.Core.Services.Logging
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }
    public class Logger
    {
        public static void Log(string msg, LogType type = LogType.Error)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}