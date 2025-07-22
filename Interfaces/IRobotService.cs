using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotService
{
    Task<robotinfo> Insert(robotinfo robot);
    Task<robotinfo> Update(robotinfo robot);
    Task Delete(string id);
    Task<robotinfo> Get(string id);

    //IEnumerable<RobotInfoDto> GetAllRobots();
    //void AddRobot(RobotInfoDto robotDto);
    //void EditRobot(int id, RobotInfoDto robotDto);
}
