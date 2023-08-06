using System.Collections.Generic;

namespace ScheduleApi.Infrastructure.Entitys
{
    public class Schedule
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }

        public virtual List<Slot> Slots { get; set; }
    }
}
