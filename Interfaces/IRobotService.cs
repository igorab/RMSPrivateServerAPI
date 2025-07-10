using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces
{
    public interface IRobotService
    {
        Task<Robot> Insert(Robot robot);
        Task<Robot> Update(Robot robot);
        Task Delete(int id);
        Task<Robot> Get(int id);
    }
}
