using System.Runtime.Serialization;

namespace StirTrekConf.PortableCore.DomainLayer
{
    using System;

    [DataContract]
    public class Speaker    
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Bio { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }

        public Uri ImageUri { get { return new Uri(ImageUrl);} }
    }
}