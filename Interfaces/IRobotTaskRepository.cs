using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Robot Task Repository
/// </summary>
public interface IRobotTaskRepository
{
    Task<IEnumerable<RobotTask>> GetAll(string robotId);

    Task<RobotTask?> Get(string robotId);

    Task<int> UpsertAsync(RobotTask robot);

    Task<int> DeleteAsync(string id);
}
