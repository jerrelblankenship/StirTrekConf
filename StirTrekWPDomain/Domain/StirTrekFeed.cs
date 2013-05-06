using System.Collections.Generic;

namespace StirTrekWPDomain.Domain
{
    public class StirTrekFeed
    {
        public List<TimeSlot> TimeSlots { get; set; }
        public List<Track> Tracks { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Sponsor> Sponsors { get; set; }

        public StirTrekFeed()
        {
            TimeSlots = new List<TimeSlot>();
            Tracks = new List<Track>();
            Speakers = new List<Speaker>();
            Sessions = new List<Session>();
        }
    }
}