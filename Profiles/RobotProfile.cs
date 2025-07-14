using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Profiles;

public class RobotProfile : Profile
{
    public RobotProfile() 
    {
        CreateMap<RobotInfoDto, RobotInfo>()
            .ForMember(robot => robot.Id, robot => robot.MapFrom(robotDto => robotDto.Id))
            .ForMember(robot => robot.Name, robot => robot.MapFrom(robotDto => robotDto.Name))                
            .ForMember(robot => robot.Description, robot => robot.MapFrom(robotDto => robotDto.Description))
            .ReverseMap();
    }

}
