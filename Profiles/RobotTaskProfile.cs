using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Profiles;

public class RobotTaskProfile : Profile
{
    public RobotTaskProfile()
    {
        CreateMap<RobotTaskDto, RobotTask>()
            .ForMember(tsk => tsk.TaskId, task => task.MapFrom(robotDto => robotDto.TaskId))
            .ForMember(tsk => tsk.RobotId, task => task.MapFrom(robotDto => robotDto.RobotId))
            .ForMember(tsk => tsk.Title, task => task.MapFrom(robotDto => robotDto.Title))
            .ForMember(tsk => tsk.Actions, task => task.MapFrom(robotDto => robotDto.Actions))
            .ReverseMap();
    }
}
