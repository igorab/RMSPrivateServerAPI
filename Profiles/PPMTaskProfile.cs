using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Profiles;

/// <summary>
/// PPM Task
/// </summary>
public class PPMTaskProfile : Profile
{
    /// <summary>
    /// PPM Task
    /// </summary>
    public PPMTaskProfile() 
    {
        CreateMap<PPMTaskDto, ppm_task>()
            .ForMember(tsk => tsk.id, task => task.MapFrom(robotDto => robotDto.Id))
            .ForMember(tsk => tsk.RobotId, task => task.MapFrom(robotDto => robotDto.RobotId))                
            .ForMember(tsk => tsk.TaskDescription, task => task.MapFrom(robotDto => robotDto.TaskDescription))
            .ReverseMap();
    }

}
