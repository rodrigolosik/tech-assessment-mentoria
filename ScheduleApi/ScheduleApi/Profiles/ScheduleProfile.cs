using AutoMapper;
using ScheduleApi.Infrastructure.Entitys;
using ScheduleApi.Models;

namespace ScheduleApi.Profiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<SchedulesDto, Schedule>().ReverseMap();
        }
    }
}
