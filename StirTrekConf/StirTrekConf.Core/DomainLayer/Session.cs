
namespace StirTrekWPDomain.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;


    public class Session
    {
        public Session()
        {
            Speakers = new List<Speaker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abstract { get; set; }

        public List<int> SpeakerIds { get; set; }
        public List<Speaker> Speakers { get; set; } 

        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
        
        [DefaultValue(-99)]
        public int TrackId { get; set; }
        public Track Track { get; set; }
        
        public List<string> Tags { get; set; }

        public string DisplayTags
        {
            get
            {
                var result = Tags.Select(tag => "#" + tag).OrderBy(x=> x).ToList();
                return string.Join("  ", result);
            }
        }

        public string DisplaySpeakers
        {
            get
            {
                return Speakers != null 
                    ? string.Join(" | ", Speakers) 
                    : string.Empty;
            }
        }

        public string DisplayTrackInfo
        {
            get
            {
                return Track != null
                    ? string.Format("{0} ({1})", Track.Name, Track.Location)
                    : string.Empty;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}