namespace BlogiFire.Core.Data
{
    public static class AppSettings
    {
        public static bool UseInMemoryDb { get; set; }
        public static bool InitializeData { get; set; }
        public static string ConnectionString { get; set; }
        public static string RelativeUrl { get; set; }
        public static string AbsoluteUrl { get; set; }
        public static int PageSize { get; set; }
        public static string Title { get; set; }
        public static string Description { get; set; }
    }
}