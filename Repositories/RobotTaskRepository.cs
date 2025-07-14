using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

namespace RMSPrivateServerAPI.Repositories
{
    public class RobotTaskRepository : IRobotTaskRepository
    {
        Task<int> IRobotTaskRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<RobotTask?> IRobotTaskRepository.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<RobotTask>> IRobotTaskRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<int> IRobotTaskRepository.UpsertAsync(RobotTask robot)
        {
            throw new NotImplementedException();
        }
    }
}
