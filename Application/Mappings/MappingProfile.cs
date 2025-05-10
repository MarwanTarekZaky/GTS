using AutoMapper;
using TodoManagementAPI.Application.DTOs;
using TodoManagementAPI.Domain.Entities;
using TodoManagementAPI.Domain.Entities.Enums;


namespace TodoManagementAPI.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<TodoStatus>(src.Status)))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TodoPriority>(src.Priority)));
    }
}