using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Profiles;

public class PPMTaskProfile : Profile
{
    public PPMTaskProfile() 
    {
        CreateMap<PPMTaskDto, PPMTask>()
            .ForMember(tsk => tsk.Id, task => task.MapFrom(robotDto => robotDto.Id))
            .ForMember(tsk => tsk.RobotId, task => task.MapFrom(robotDto => robotDto.RobotId))                
            .ForMember(tsk => tsk.TaskDescription, task => task.MapFrom(robotDto => robotDto.TaskDescription))
            .ReverseMap();
    }

}
