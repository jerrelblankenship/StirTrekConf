using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StirTrekConf.Core.WebServiceLayer
{
    using StirTrekWPDomain.Domain;

    public class RestProcessor
    {
        private string jsonFeedUrl = @"http://stirtrek.com/Feed/JSON";
        private string feedLastUpdatedUrl = @"http://stirtrek.com/lastupdate";

        public StirTrekFeed GetStirTrekFeed()
        {
            return new StirTrekFeed();
        }
    }
}
