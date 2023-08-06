using System.Collections.Generic;
using System.Threading.Tasks;
using ScheduleApi.Infrastructure.Entitys;

namespace ScheduleApi.Infrastructure
{
    public interface IScheduleRepository : IRepositoryAsync<Schedule>
    {
        Task<Schedule> GetSchedule(string name, string role);

        Task<List<Slot>> GetSlots(string candidateName, List<string> interviewerNames);

    }
}
