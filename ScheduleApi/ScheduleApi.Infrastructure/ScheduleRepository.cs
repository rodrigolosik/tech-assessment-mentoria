using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScheduleApi.Infrastructure.Entitys;

namespace ScheduleApi.Infrastructure
{
    public class ScheduleRepository : RepositoryAsync<Schedule>, IScheduleRepository
    {
        private readonly DataContext dataContext;
        public ScheduleRepository(DataContext _dbContext) : base(_dbContext)
        {
            dataContext = _dbContext;
        }

        public async Task<List<Slot>> GetSlots(string candidateName, List<string> interviewerNames)
        {
            var result = new List<Slot>();
            var candidateSchedule = await GetSchedule(candidateName, "Candidate");
            var all = new List<List<Slot>>();
            // for each interviewer create a list of slots
            foreach (var name in interviewerNames)
            {
                var interviewerSchedule = await GetSchedule(name, "Interviewer");
                all.Add(interviewerSchedule.Slots);
            }
            List<Slot> commonInterviewerSlots =
                all.Aggregate((l1, l2) => l1.Intersect(l2, new SlotComparer()).ToList()).ToList();

            foreach (var item in candidateSchedule.Slots)
            {
                var interSection = commonInterviewerSlots
                        .FirstOrDefault(c => c.DateEnd == item.DateEnd
                        && c.DateStart == item.DateStart);
                if (interSection != null)
                {
                    result.Add(interSection);
                }

            }
            return result.OrderBy(c => c.DateStart).ToList();
        }

        public async Task<Schedule> GetSchedule(string name, string role)
        {
            return await dataContext.Schedules
                .Include(c => c.Slots)
                .Where(c => c.Name == name && c.Role == role)
                .SingleOrDefaultAsync();
        }

    }
}
