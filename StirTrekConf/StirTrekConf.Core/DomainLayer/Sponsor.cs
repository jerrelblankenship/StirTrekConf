namespace StirTrekWPDomain.Domain
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string URL { get; set; }
        public string SponsorType { get; set; }
        public string GetProperLogoUrl
        {
            get
            {
                if (LogoUrl.StartsWith("/Content"))
                {
                    return "http://stirtrek.com" + LogoUrl;
                }
                return LogoUrl;
            }
        }
    }
}