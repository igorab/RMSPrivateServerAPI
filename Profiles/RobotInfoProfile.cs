using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Profiles;
#pragma warning disable CS1591


public class RobotInfoProfile : Profile
{
    public RobotInfoProfile() 
    {
        CreateMap<RobotInfoDto, robotinfo>()
            .ForMember(robot => robot.RobotId, robot => robot.MapFrom(robotDto => robotDto.RobotId))
            .ForMember(robot => robot.robothardwareid, robot => robot.MapFrom(robotDto => robotDto.RobotHardwareId))                                       
            .ForMember(robot => robot.robottype, robot => robot.MapFrom(robotDto => robotDto.RobotType))
            .ForMember(robot => robot.robotmodel, robot => robot.MapFrom(robotDto => robotDto.RobotModel))
            .ForMember(robot => robot.robotname, robot => robot.MapFrom(robotDto => robotDto.RobotName))
            .ForMember(robot => robot.ip, robot => robot.MapFrom(robotDto => robotDto.IP))
            .ForMember(robot => robot.swversion, robot => robot.MapFrom(robotDto => robotDto.SwVersion))
            .ForMember(robot => robot.hwversion, robot => robot.MapFrom(robotDto => robotDto.HwVersion))
            .ForMember(robot => robot.is_deleted, robot => robot.MapFrom(robotDto => robotDto.is_deleted))

            .ReverseMap();
    }

}
