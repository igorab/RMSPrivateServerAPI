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

        Task<RobotTask> IRobotTaskService.Get(int id)
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

        Task IRobotTaskService.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
