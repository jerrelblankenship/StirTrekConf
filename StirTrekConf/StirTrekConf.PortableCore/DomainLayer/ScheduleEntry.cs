namespace StirTrekConf.PortableCore.DomainLayer
{
    using System.Collections.Generic;

    public class ScheduleEntry 
    {
        public TimeSlot TimeSlot { get; set; }
        public List<Session> Sessions { get; set; } 
    }
}