using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Profiles;

/// <summary>
/// Маппинг
/// </summary>
public class RobotTaskProfile : Profile
{
    /// <summary>
    /// Маппинг POCO-DTOs
    /// </summary>
    public RobotTaskProfile()
    {
        CreateMap<RobotTaskDto, robot_task>()
            .ForMember(tsk => tsk.task_id, task => task.MapFrom(robotDto => robotDto.TaskId))
            .ForMember(tsk => tsk.robot_id, task => task.MapFrom(robotDto => robotDto.RobotId))
            .ForMember(tsk => tsk.title, task => task.MapFrom(robotDto => robotDto.Title))
            .ForMember(tsk => tsk.actions, task => task.MapFrom(robotDto => robotDto.Actions))
            .ReverseMap();
    }
}
