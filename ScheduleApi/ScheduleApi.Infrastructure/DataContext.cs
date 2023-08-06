using Microsoft.EntityFrameworkCore;
using ScheduleApi.Infrastructure.Entitys;

namespace ScheduleApi.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }

        public DataContext()
        {

        }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }
    }
}
