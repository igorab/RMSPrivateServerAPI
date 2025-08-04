using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Interfaces;

public interface IPPMService
{
    public IEnumerable<PPMTaskDto> GetTasksByRobotId(Guid RobotId);

    public void AddPPMTask(PPMTaskDto ppmTaskDto);

    public void EditPPMTask(Guid ppmTaskId, PPMTaskDto ppmTaskDto);        
}
