namespace StirTrekConf.PortableCore.DomainLayer
{
    using System;

    public class TimeSlot
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string DisplayTimeRange
        {
            get { return string.Format("{0} - {1}", StartTime.ToLocalTime(), EndTime.TimeOfDay); }
        }
    }
}