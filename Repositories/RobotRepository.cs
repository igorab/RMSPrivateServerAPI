using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using Dapper;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Enums;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Repositories;

public class RobotRepository : IRobotRepository
{
    private readonly DatabaseConnectionFactory _databaseConnectionFactory;

    public RobotRepository(DatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }
    
    public async Task<IEnumerable<robot_info>> GetAll(bool returnDeletedRecords)
    {
        var builder = new SqlBuilder();

        var sqlTemplate = builder.AddTemplate(
            @"SELECT * FROM ""RobotInfo""");
            
        if (!returnDeletedRecords)
        {
            builder.Where(@"""RobotState"" = 0");
        }

        using var db = _databaseConnectionFactory.GetConnection();

        return await db.QueryAsync<robot_info>(sqlTemplate.RawSql, sqlTemplate.Parameters);
    }

    public async Task<robot_info?> Get(Guid robotId)
    {        
        using var db = _databaseConnectionFactory.GetConnection();

        var sql =
            $@"SELECT * FROM ""RobotInfo"" RI WHERE 
                RI.""RobotId"" = @{nameof(robotId)}
                AND RI.""RobotState"" = {(int)RobotState.online}";

        var param = new {robotId};

        var robot = await db.QueryFirstOrDefaultAsync<robot_info>(sql, param);

        return robot;
    }

    public async Task<robot_info?> GetByHardwareId(int hardId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql =
            $@"SELECT * FROM ""RobotInfo"" RI WHERE 
                RI.""RobotHardwareId"" = @{nameof(hardId)}";
                //AND RI.""RobotState"" = @{(int)RobotState.online}";

        var param = new { hardId };

        var robot = await db.QueryFirstOrDefaultAsync<robot_info>(sql, param);

        return robot;

    }

    public async Task<Guid> UpsertAsync(robot_info robot)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"
                INSERT INTO ""RobotInfo"" (""RobotId"", ""RobotHardwareId"", ""RobotType"", ""RobotModel"", ""RobotName"", ""IP"", ""SwVersion"", ""HwVersion"", ""RobotState"")
                    VALUES (@RobotId, @robothardwareid, @robottype, @robotmodel, @robotname, @ip, @swversion, @hwversion, @robot_state)
                    ON CONFLICT (""RobotId"") DO UPDATE
                    SET                         
                        ""RobotHardwareId"" = EXCLUDED.""RobotHardwareId"",
                        ""RobotType""       = EXCLUDED.""RobotType"",
                        ""RobotModel""      = EXCLUDED.""RobotModel"",
                        ""RobotName""       = EXCLUDED.""RobotName"",
                        ""IP""              = EXCLUDED.""IP"",
                        ""SwVersion""       = EXCLUDED.""SwVersion"",
                        ""HwVersion""       = EXCLUDED.""HwVersion"",
                        ""RobotState""      = EXCLUDED.""RobotState""
                    RETURNING ""RobotId"";
            ";

        var newId = await db.QuerySingleOrDefaultAsync<Guid>(sql, robot);

        return newId == Guid.Empty ? robot.RobotId : newId;
    }

    /// <summary>
    /// Need refactoring! сделать смену состояния
    /// </summary>
    /// <param name="robot_id"></param>
    /// <returns></returns>
    public async Task<int> DeleteAsync(Guid robot_id)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = $@"Update ""RobotInfo"" SET ""RobotState"" = {(int)RobotState.offline} WHERE ""RobotId"" = @id";

        return await db.ExecuteAsync(query, new { id = robot_id });
    }

   
}
