using System.Collections.Generic;

namespace ScheduleApi.Models
{
    public class SchedulesDto
    {
        public string Name { get; set; }

        public string Role { get; set; }

        public List<SlotDto> Slots { get; set; }
    }
}
