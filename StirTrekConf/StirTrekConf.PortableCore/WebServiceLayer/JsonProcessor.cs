using System.Runtime.Serialization.Json;

namespace StirTrekConf.PortableCore.WebServiceLayer
{
    using System;
    using System.IO;
    using DomainLayer;
    
    public class JsonProcessor : IJsonProcessor
    {
        public StirTrekFeed DescerializeJsonFeed(string content)
        {
            var feed = new StirTrekFeed();
            var streamReader = StringToStreamReader(content);
            try
            {
                var feedObj = new DataContractJsonSerializer(typeof(StirTrekFeed));
                feed = (StirTrekFeed) feedObj.ReadObject(streamReader);
            }
            catch (Exception e)
            {
                int i = 0;
            }
            return feed;
        }

        public DateTime DescerializeLastUpdated(string content)
        {
            var streamReader = StringToStreamReader(content);
            //return (DateTime) _serializer.Deserialize(streamReader, typeof (DateTime));
            return DateTime.Now;
        }

        internal Stream StringToStreamReader(string content)
        {
            byte[] byteArray = StringToAscii(content);
            var stream = new MemoryStream(byteArray);
            return stream;
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
