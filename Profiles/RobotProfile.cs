using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Profiles;

public class RobotProfile : Profile
{
    public RobotProfile() 
    {
        CreateMap<RobotInfoDto, RobotInfo>()
            .ForMember(robot => robot.RobotId, robot => robot.MapFrom(robotDto => robotDto.RobotId))
            .ForMember(robot => robot.RobotHardwareID, robot => robot.MapFrom(robotDto => robotDto.RobotHardwareID))                                       
            .ForMember(robot => robot.RobotType, robot => robot.MapFrom(robotDto => robotDto.RobotType))
            .ForMember(robot => robot.RobotModel, robot => robot.MapFrom(robotDto => robotDto.RobotModel))
            .ForMember(robot => robot.RobotName, robot => robot.MapFrom(robotDto => robotDto.RobotName))
            .ForMember(robot => robot.IP, robot => robot.MapFrom(robotDto => robotDto.IP))
            .ForMember(robot => robot.SwVersion, robot => robot.MapFrom(robotDto => robotDto.SwVersion))
            .ForMember(robot => robot.HwVersion, robot => robot.MapFrom(robotDto => robotDto.HwVersion))

            .ReverseMap();
    }

}
