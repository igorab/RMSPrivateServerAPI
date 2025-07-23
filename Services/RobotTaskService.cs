using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

namespace RMSPrivateServerAPI.Services
{
    public class RobotTaskService : IRobotTaskService
    {
        private readonly IRobotTaskRepository _robotTaskRepository;

        public RobotTaskService(IRobotTaskRepository robotTaskRepository)
        {
            _robotTaskRepository = robotTaskRepository;
        }
        
        Task<RobotTask> IRobotTaskService.Get(string robotId)
        {
            throw new NotImplementedException();
        }

        Task<RobotTask> IRobotTaskService.Insert(RobotTask task)
        {
            throw new NotImplementedException();
        }

        Task<RobotTask> IRobotTaskService.Update(RobotTask task)
        {
            throw new NotImplementedException();
        }

        Task IRobotTaskService.Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
