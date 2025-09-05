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
            .ForMember(tsk => tsk.TaskId, task => task.MapFrom(robotDto => robotDto.TaskId))
            .ForMember(tsk => tsk.RobotId, task => task.MapFrom(robotDto => robotDto.RobotId))
            .ForMember(tsk => tsk.Title, task => task.MapFrom(robotDto => robotDto.Title))            
            .ReverseMap();

        CreateMap<List<RobotActions>, RobotTaskDto>()
            .ForPath(dest => dest.TaskId, ac => ac.MapFrom(src => src.First().TaskId))
            .ForPath(dest => dest.RobotId, ac => ac.MapFrom(src => src.First().RobotId))
            .ForPath(dest => dest.Title, ac => ac.MapFrom(src => src.First().Result))
            .ForPath(dest => dest.RobotActionsDto, ac => ac.MapFrom(src => src));

        CreateMap<RobotActionsDto, RobotActions>()
            .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.ActionId))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.RobotId, opt => opt.MapFrom(src => src.RobotId))
            .ForMember(dest => dest.ActionIndex, opt => opt.MapFrom(src => src.ActionIndex))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason)) // << enough  ? 
            .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => src.ActionType))
            .ForMember(dest => dest.Pose_X, opt => opt.MapFrom(src => src.Pose_X))
            .ForMember(dest => dest.Pose_Y, opt => opt.MapFrom(src => src.Pose_Y))
            .ForMember(dest => dest.Heading, opt => opt.MapFrom(src => src.Heading))
            .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
            .ForMember(dest => dest.Angle, opt => opt.MapFrom(src => src.Angle))
            .ForMember(dest => dest.Radius, opt => opt.MapFrom(src => src.Radius))
            .ReverseMap();            
    }
}
