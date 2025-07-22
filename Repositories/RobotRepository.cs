using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using Dapper;
using RMSPrivateServerAPI.Data;

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

    public async Task<IEnumerable<robotinfo>> GetAll(bool returnDeletedRecords)
    {
        var builder = new SqlBuilder();

        var sqlTemplate = builder.AddTemplate(
            "SELECT * FROM robotinfo " +
            "/**where**/ ");

        if (!returnDeletedRecords)
        {
            builder.Where("is_deleted=0");
        }

        using var db = _databaseConnectionFactory.GetConnection();

        return await db.QueryAsync<robotinfo>(sqlTemplate.RawSql, sqlTemplate.Parameters);
    }

    public async Task<robotinfo?> Get(string robotId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql =
            $@"SELECT * 
                    FROM  
                robotinfo RI 
                    WHERE 
                RI.robotid = @{nameof(robotId)}
                AND RI.is_deleted = 0";

        var param = new {robotId};

        var robot = await db.QueryFirstOrDefaultAsync<robotinfo>(sql, param);

        return robot;
    }
    
    public async Task<string> UpsertAsync(robotinfo robot)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"
                NSERT INTO robotinfo (robotid, robothardwareid, robottype, robotmodel, robotname, ip, swversion, hwversion, is_deleted)
                    VALUES (@robotid, @robothardwareid, @robottype, @robotmodel, @robotname, @ip, @swversion, @hwversion, @is_deleted)
                    ON CONFLICT (robotid) DO UPDATE
                    SET                         
                        robothardwareid = EXCLUDED.robothardwareid,
                        robottype       = EXCLUDED.robottype,
                        robotmodel      = EXCLUDED.robotmodel,
                        robotname       = EXCLUDED.robotname,
                        ip              = EXCLUDED.ip,
                        swversion       = EXCLUDED.swversion,
                        hwversion       = EXCLUDED.hwversion,
                        is_deleted      = EXCLUDED.is_deleted
                    RETURNING robotid;
            ";

        var newId = await db.QuerySingleOrDefaultAsync<string>(sql, robot);
        return newId == String.Empty ? robot.robotid : newId;
    }

    public async Task<int> DeleteAsync(string robot_id)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = "Update robotinfo SET is_deleted = 1 WHERE robotid = @id";

        return await db.ExecuteAsync(query, new { id = robot_id });
    }
}
