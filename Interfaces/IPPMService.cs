using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces
{
    public interface IPPMService
    {
        public IEnumerable<PPMTaskDto> GetTasksByRobotId(string RobotId);

        public void AddPPMTask(PPMTaskDto ppmTaskDto);

        public void EditPPMTask(int ppmTaskId, PPMTaskDto ppmTaskDto);        
    }
}
