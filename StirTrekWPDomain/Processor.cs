namespace StirTrekWPDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using System.IO;
    using Newtonsoft.Json;

    public class Processor
    {
        private AppSettingsStorage _settingsStorage;

        public Processor()
        {
            _settingsStorage = new AppSettingsStorage();
        }

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
            streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            _settingsStorage.DefaultJsonData = streamReader.ReadToEnd();
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
                session.TimeSlot = returnFeed.TimeSlots.FirstOrDefault(x => x.Id == session.TimeSlotId);
            }
        }

        public List<ScheduleEntry> GenerateSchedule(StirTrekFeed feed)
        {
            var scheduleList = new List<ScheduleEntry>();
            foreach (var timeSlot in feed.TimeSlots)
            {
                var sessionList = feed.Sessions
                    .Where(x => x.TimeSlotId == timeSlot.Id)
                    .OrderBy(x => x.TrackId)
                    .ToList();

                scheduleList.Add(new ScheduleEntry{TimeSlot = timeSlot, Sessions = sessionList});
            }

            return scheduleList.OrderBy(x => x.TimeSlot.StartTime).ToList();
        }

        public bool CheckCacheValue(Stream result)
        {
            var streamReader = new StreamReader(result);

            var json = streamReader.ReadToEnd();

            var dateLastUpdated = (DateTime)JsonConvert.DeserializeObject(json);
            var lastUpdateDate = _settingsStorage.LastUpdated;

            if (lastUpdateDate == DateTime.MinValue)
            {
                _settingsStorage.LastUpdated = dateLastUpdated;
                return true;
            }
            
            if (dateLastUpdated > lastUpdateDate)
            {
                _settingsStorage.LastUpdated = dateLastUpdated;
                return true;
            }
            return false;
        }

        public StirTrekFeed LoadDefaultStirTrekData()
        {
            var json = _settingsStorage.DefaultJsonData;

            byte[] byteArray = StringToAscii(json);
            var stream = new MemoryStream(byteArray);
            var streamReader = new StreamReader(stream);

            var serializer = new JsonSerializer
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                NullValueHandling = NullValueHandling.Ignore
            };

            var returnFeed = (StirTrekFeed)serializer.Deserialize(streamReader, typeof(StirTrekFeed));

            LoadSessions(returnFeed);

            return returnFeed;
        }

        public static byte[] StringToAscii(string s)
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

        public void LoadInitialJsonData(Stream defaultStream)
        {
            if (string.IsNullOrEmpty(_settingsStorage.DefaultJsonData))
            {
                var json = new StreamReader(defaultStream).ReadToEnd();
                _settingsStorage.DefaultJsonData = json;
            }
        }
    }
} 
