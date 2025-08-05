using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using Dapper;
using RMSPrivateServerAPI.Data;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Repositories;

public class RobotRepository : IRobotRepository
{
    private readonly DatabaseConnectionFactory _databaseConnectionFactory;

    public RobotRepository(DatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    private async Task<T> QueryFirstOrDefaultAsync<T>(string sql, params object[] param )
    {
        using var db = _databaseConnectionFactory.GetConnection();

        return await db.QueryFirstOrDefaultAsync<T>(sql, param);
    }

    public async Task<IEnumerable<robot_info>> GetAll(bool returnDeletedRecords)
    {
        var builder = new SqlBuilder();

        var sqlTemplate = builder.AddTemplate(
            @"SELECT * FROM ""RobotInfo""");
            
        if (!returnDeletedRecords)
        {
            builder.Where(@"""Is_Deleted"" = 0");
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
                AND RI.""Is_Deleted"" = 0";

        var param = new {robotId};

        var robot = await db.QueryFirstOrDefaultAsync<robot_info>(sql, param);

        return robot;
    }
    
    public async Task<Guid> UpsertAsync(robot_info robot)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"
                INSERT INTO ""RobotInfo"" (""RobotId"", ""RobotHardwareId"", ""RobotType"", ""RobotModel"", ""RobotName"", ""IP"", ""SwVersion"", ""HwVersion"", ""Is_Deleted"")
                    VALUES (@RobotId, @robothardwareid, @robottype, @robotmodel, @robotname, @ip, @swversion, @hwversion, @is_deleted)
                    ON CONFLICT (""RobotId"") DO UPDATE
                    SET                         
                        ""RobotHardwareId"" = EXCLUDED.""RobotHardwareId"",
                        ""RobotType""       = EXCLUDED.""RobotType"",
                        ""RobotModel""      = EXCLUDED.""RobotModel"",
                        ""RobotName""       = EXCLUDED.""RobotName"",
                        ""IP""              = EXCLUDED.""IP"",
                        ""SwVersion""       = EXCLUDED.""SwVersion"",
                        ""HwVersion""       = EXCLUDED.""HwVersion"",
                        ""Is_Deleted""      = EXCLUDED.""Is_Deleted""
                    RETURNING ""RobotId"";
            ";

        var newId = await db.QuerySingleOrDefaultAsync<Guid>(sql, robot);

        return newId == Guid.Empty ? robot.RobotId : newId;
    }

    public async Task<int> DeleteAsync(Guid robot_id)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = @"Update ""RobotInfo"" SET ""Is_Deleted"" = 1 WHERE ""RobotId"" = @id";

        return await db.ExecuteAsync(query, new { id = robot_id });
    }
}
