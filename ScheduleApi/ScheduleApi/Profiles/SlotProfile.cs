using AutoMapper;
using ScheduleApi.Infrastructure.Entitys;
using ScheduleApi.Models;

namespace ScheduleApi.Profiles
{
    public class SlotProfile : Profile
    {
        public SlotProfile()
        {
            CreateMap<SlotDto, Slot>().ReverseMap();
        }
    }
}
