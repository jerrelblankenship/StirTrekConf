namespace StirTrekConf.PortableCore.DomainLayer
{
    using System;

    public class Speaker    
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        public string ImageUrl { get; set; }

        public Uri ImageUri { get { return new Uri(ImageUrl);} }
    }
}