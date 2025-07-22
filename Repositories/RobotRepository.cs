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

    public async Task<robotinfo?> Get(int robotId)
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
    
    public async Task<int> UpsertAsync(robotinfo robot)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"
                DECLARE @InsertedRows AS TABLE (RobotId int);
                MERGE INTO RobotInfo AS target
                USING (SELECT @RobotId AS RobotId, @RobotHardwareID AS RobotHardwareID, @RobotType as RobotType, 
                @RobotModel as RobotModel, @RobotName as RobotName, @IP as IP,
                @SwVersion as swVersion, @HwVersion as hwVersion, @is_deleted AS is_deleted ) AS source 
                ON target.RobotId = source.RobotId
                WHEN MATCHED THEN 
                    UPDATE SET                         
                        RobotHardwareID = source.RobotHardwareID,
                        RobotType       = source.RobotType,
                        RobotModel      = source.RobotModel,
                        RobotName       = source.RobotName,
                        IP              = source.IP,
                        SwVersion       = source.swVersion,
                        HwVersion       = source.hwVersion,
                        is_deleted      = source.is_deleted
                WHEN NOT MATCHED THEN
                    INSERT (RobotId, RobotHardwareID, RobotType, RobotModel, RobotName, IP, SwVersion, HwVersion, is_deleted)
                    VALUES (source.RobotId, source.RobotHardwareID, source.RobotType, source.RobotModel, source.RobotName, source.IP, source.swVersion, source.hwVersion, 
                    source.is_deleted)
                    OUTPUT inserted.RobotId INTO @InsertedRows
                ;
                SELECT RobotId FROM @InsertedRows;
            ";

        var newId = await db.QuerySingleOrDefaultAsync<int>(sql, robot);
        return newId == 0 ? robot.robotid : newId;
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = "Update robotinfo SET is_deleted = 1 WHERE robotid = @Id";

        return await db.ExecuteAsync(query, new { Id = id });
    }
}
