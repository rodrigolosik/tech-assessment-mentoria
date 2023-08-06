using System;

namespace ScheduleApi.Infrastructure.Entitys
{
    public class Slot
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
