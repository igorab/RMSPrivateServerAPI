using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

namespace RMSPrivateServerAPI.Repositories
{
    public class RobotRepository : IRobotRepository
    {
        Task<int> IRobotRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Robot?> IRobotRepository.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Robot>> IRobotRepository.GetAll(bool returnDeletedRecords)
        {
            throw new NotImplementedException();
        }

        Task<int> IRobotRepository.UpsertAsync(Robot robot)
        {
            throw new NotImplementedException();
        }
    }
}
