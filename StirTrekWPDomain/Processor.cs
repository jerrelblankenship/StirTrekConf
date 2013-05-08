namespace StirTrekWPDomain
{
    using System.Collections.Generic;
    using System.Linq;
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
            var returnFeed = (StirTrekFeed) serializer.Deserialize(streamReader, typeof (StirTrekFeed));

            LoadSessions(returnFeed);

            return returnFeed;
        }

        private void LoadSessions(StirTrekFeed returnFeed)
        {
            foreach (var session in returnFeed.Sessions)
            {
                session.Speakers = new List<Speaker>();

                foreach (var speaker in 
                    session.SpeakerIds.Select(speakerId => returnFeed.Speakers.FirstOrDefault(x => x.Id == speakerId))
                    .Where(speaker => speaker != null))
                {
                    session.Speakers.Add(speaker);
                }

                session.Track = returnFeed.Tracks.FirstOrDefault(x => x.Id == session.TrackId);
            }
        }

        public List<ScheduleEntry> GenerateSchedule(StirTrekFeed feed)
        {
            var scheduleList = new List<ScheduleEntry>();
            foreach (var timeSlot in feed.TimeSlots)
            {
                var sessionList = feed.Sessions.Where(x => x.TimeSlotId == timeSlot.Id).ToList();
                scheduleList.Add(new ScheduleEntry{TimeSlot = timeSlot, Sessions = sessionList});
            }

            return scheduleList.OrderBy(x => x.TimeSlot.StartTime).ToList();
        }
    }
}
