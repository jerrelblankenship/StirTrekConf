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
            var resturnObject = (StirTrekFeed) serializer.Deserialize(streamReader, typeof (StirTrekFeed));
            return resturnObject;
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

    public class ScheduleEntry 
    {
        public TimeSlot TimeSlot { get; set; }
        public List<Session> Sessions { get; set; } 
    }
}
