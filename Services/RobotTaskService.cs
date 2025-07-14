using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

namespace RMSPrivateServerAPI.Services
{
    public class RobotTaskService : IRobotTaskService
    {
        Task IRobotTaskService.Delete(int id)
        {
            throw new NotImplementedException();
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
    }
}
