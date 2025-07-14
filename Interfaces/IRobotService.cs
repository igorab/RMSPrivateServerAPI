using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces
{
    public interface IRobotService
    {
        Task<RobotInfo> Insert(RobotInfo robot);
        Task<RobotInfo> Update(RobotInfo robot);
        Task Delete(int id);
        Task<RobotInfo> Get(int id);
    }
}
