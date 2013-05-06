namespace StirTrekWPDomain.Domain
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string DisplayTimeRange
        {
            get { return string.Format("{0} -- {1}", StartTime, EndTime); }
        }
    }
}