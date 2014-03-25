using System;
using StirTrekConf.WinPhone.Model;

namespace StirTrekConf.WinPhone.Design
{
    using System.Collections.Generic;
    using PortableCore.DomainLayer;

    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }

        public void GetSpeakers(Action<List<Speaker>, Exception> callback)
        {
            var speakers = new List<Speaker>();

            var speaker = new Speaker()
            {
                Bio = "Some Bio Information",
                Id = 1,
                Name = "Anthony van der Hoorn",
                ImageUrl = "http://stirtrek.com/Content/Images/Speakers/Jerrel-Blankenship.png"
            };

            speakers.Add(speaker);
            speakers.Add(speaker);

            callback(speakers, null);
        }
    }
}