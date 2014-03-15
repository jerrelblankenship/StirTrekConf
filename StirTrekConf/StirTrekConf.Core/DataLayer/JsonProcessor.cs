namespace StirTrekConf.Core.DataLayer
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using StirTrekWPDomain.Domain;

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
            byte[] byteArray = StringToAscii(content);
            var stream = new MemoryStream(byteArray);
            var streamReader = new StreamReader(stream);

            var feed = (StirTrekFeed) _serializer.Deserialize(streamReader, typeof (StirTrekFeed));

            return feed;
        }

        public DateTime DescerializeLastUpdated(string content)
        {
            var streamReader = new StreamReader(content);
            return (DateTime) _serializer.Deserialize(streamReader, typeof (DateTime));
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
