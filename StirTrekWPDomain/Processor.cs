namespace StirTrekWPDomain
{
    using Domain;
    using System.IO;
    using Newtonsoft.Json;

    public class Processor
    {
        public StirTrekFeed LoadStirTrekData(Stream webStream)
        {
            var streamReader = new StreamReader(webStream);
            var serializer = new JsonSerializer
                {
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    NullValueHandling = NullValueHandling.Ignore
                };
            var resturnObject = (StirTrekFeed) serializer.Deserialize(streamReader, typeof (StirTrekFeed));
            return resturnObject;
        }
    }
}
