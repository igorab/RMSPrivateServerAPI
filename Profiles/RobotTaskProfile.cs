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
            .ReverseMap();

        CreateMap<List<RobotTaskFlat>, RobotTaskDto>()
            .ForPath(dest => dest.TaskId, ac => ac.MapFrom(src => src.First().robot_task_id))
            .ForPath(dest => dest.RobotId, ac => ac.MapFrom(src => src.First().robot_id))
            .ForPath(dest => dest.Title, ac => ac.MapFrom(src => src.First().title))
            .ForPath(dest => dest.RobotActionsDto, ac => ac.MapFrom(src => src));

        CreateMap<RobotActionsDto, RobotTaskFlat>()
            .ForMember(dest => dest.action_id, opt => opt.MapFrom(src => src.ActionId))
            .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.action_type, opt => opt.MapFrom(src => src.ActionType))
            .ForMember(dest => dest.pose_x, opt => opt.MapFrom(src => src.Pose_X))
            .ForMember(dest => dest.pose_y, opt => opt.MapFrom(src => src.Pose_Y))
            .ForMember(dest => dest.heading, opt => opt.MapFrom(src => src.Heading))
            .ForMember(dest => dest.direction, opt => opt.MapFrom(src => src.Direction))
            .ForMember(dest => dest.distance, opt => opt.MapFrom(src => src.Distance))
            .ForMember(dest => dest.angle, opt => opt.MapFrom(src => src.Angle))
            .ForMember(dest => dest.radius, opt => opt.MapFrom(src => src.Radius))
            .ReverseMap();            
    }
}
