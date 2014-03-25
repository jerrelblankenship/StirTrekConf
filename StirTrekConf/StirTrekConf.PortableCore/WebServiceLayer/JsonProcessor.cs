namespace StirTrekConf.PortableCore.WebServiceLayer
{
    using System;
    using System.IO;
    using DomainLayer;
    using Newtonsoft.Json;

    public class JsonProcessor : IJsonProcessor
    {
        private readonly JsonSerializer _serializer;

        public JsonProcessor()
        {
            _serializer = new JsonSerializer
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public StirTrekFeed DescerializeJsonFeed(string content)
        {
            var streamReader = StringToStreamReader(content);
            return (StirTrekFeed) _serializer.Deserialize(streamReader, typeof (StirTrekFeed));
        }

        public DateTime DescerializeLastUpdated(string content)
        {
            var streamReader = StringToStreamReader(content);
            return (DateTime) _serializer.Deserialize(streamReader, typeof (DateTime));
        }

        internal StreamReader StringToStreamReader(string content)
        {
            byte[] byteArray = StringToAscii(content);
            var stream = new MemoryStream(byteArray);
            return new StreamReader(stream);
        }

        internal byte[] StringToAscii(string s)
        {
            var retval = new byte[s.Length];
            for (var ix = 0; ix < s.Length; ++ix)
            {
                var ch = s[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }
    }
}
