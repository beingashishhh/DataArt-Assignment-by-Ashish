using AutoMapper;
using MyProject.Domain.Entities;
using MyProject.Domain.DTOs;

namespace MyProject.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Calendar, CalendarDto>().ReverseMap();
            CreateMap<Attendee, AttendeeDto>().ReverseMap();
        }
    }
}
