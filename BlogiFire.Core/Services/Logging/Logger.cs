using System;

namespace BlogiFire.Core.Services.Logging
{
    public class Logger
    {
        public static void Log(string msg)
        {
            System.Diagnostics.Debug.WriteLine("LOG :: " + msg);
        }
    }
}