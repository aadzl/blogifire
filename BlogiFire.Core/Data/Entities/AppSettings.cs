using System;

namespace BlogiFire.Core.Data
{
    public static class AppSettings
    {
        public static bool UseInMemoryDb { get; set; }
        public static bool InitializeData { get; set; }
        public static string ConnectionString { get; set; }
    }
}