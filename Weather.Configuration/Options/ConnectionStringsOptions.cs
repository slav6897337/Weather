namespace Weather.Configuration.Options
{
    public class ConnectionStringsOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";

        public string Url { get; set; }
        public string HeaderName { get; set; }
        public string HeaderValue { get; set; }
        public string HeaderNameCity { get; set; }
    }
}
