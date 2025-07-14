using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotTaskRepository
{
    Task<IEnumerable<RobotTask>> GetAll();

    Task<RobotTask?> Get(int id);

    Task<int> UpsertAsync(RobotTask robot);

    Task<int> DeleteAsync(int id);
}
