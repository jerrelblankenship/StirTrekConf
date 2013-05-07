
namespace StirTrekWPDomain.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel;


    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abstract { get; set; }

        public List<int> SpeakerIds { get; set; }
        public List<Speaker> Speakers { get; set; } 

        public int TimeSlotId { get; set; }
        
        [DefaultValue(-99)]
        public int TrackId { get; set; }
        public Track Track { get; set; }
        
        public List<string> Tags { get; set; }

        public string DisplayTags
        {
            get { return string.Join(",", Tags); }
        }

        public string DisplaySpeakers
        {
            get { return string.Join(" | ", Speakers); }
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}