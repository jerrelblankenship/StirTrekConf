namespace StirTrekConf.Core.DataLayer
{
    using System.IO;
    using Newtonsoft.Json;
    using StirTrekWPDomain.Domain;

    public class DataProcessor : IDataProcessor
    {
        private readonly JsonSerializer _serializer;

        public DataProcessor()
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
