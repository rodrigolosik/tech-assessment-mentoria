using ScheduleApi.Infrastructure.Entitys;

namespace ScheduleApi.Infrastructure
{
    public class SlotRepository : RepositoryAsync<Slot>, ISlotRepository
    {
        public SlotRepository(DataContext _dbContext) : base(_dbContext)
        {

        }
    }
}
