using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Profiles;

public class PPMTaskProfile : Profile
{
    public PPMTaskProfile() 
    {
        CreateMap<PPMTaskDto, ppmtask>()
            .ForMember(tsk => tsk.id, task => task.MapFrom(robotDto => robotDto.Id))
            .ForMember(tsk => tsk.robotid, task => task.MapFrom(robotDto => robotDto.RobotId))                
            .ForMember(tsk => tsk.taskdescription, task => task.MapFrom(robotDto => robotDto.TaskDescription))
            .ReverseMap();
    }

}
