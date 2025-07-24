using Dapper;
using Microsoft.IdentityModel.Tokens;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Repositories;

public class RobotTaskRepository : IRobotTaskRepository
{
    private readonly DatabaseConnectionFactory _databaseConnectionFactory;

    public RobotTaskRepository(DatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    public async Task<robot_task?> Get(string robotId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql =
                $@"SELECT * 
               FROM  
                    RobotTask t 
               WHERE 
                    t.robotid = @{nameof(robotId)}";
                
        var param = new { robotId };

        var car = await db.QueryFirstOrDefaultAsync<robot_task>(sql, param);

        return car;
    }

    public async Task<IEnumerable<robot_task>> GetAll(string robotId)
    {
        var builder = new SqlBuilder();
        var sqlTemplate = builder.AddTemplate(
            $@"SELECT * FROM RobotTask t 
               WHERE   t.robotid = @{nameof(robotId)}   ");
             
        using var db = _databaseConnectionFactory.GetConnection();

        return await db.QueryAsync<robot_task>(sqlTemplate.RawSql, sqlTemplate.Parameters);
    }

    public async Task<string> UpsertAsync(robot_task robotTask)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"
                DECLARE @InsertedRows AS TABLE (Id string);

                MERGE INTO RobotTask AS target
                USING (SELECT @RobotId AS RobotId, @TaskId AS TaskId, @Title as Ttitle, @Actions as Actions) AS source                 
                ON target.Id = source.Id
                WHEN MATCHED THEN 
                    UPDATE SET                         
                        RobotId   = source.RobotId,
                        Title     = source.Title,
                        Actions   = source.Actions                        
                WHEN NOT MATCHED THEN
                    INSERT (RobotId, Title, Actions)
                    VALUES (source.RobotId, source.Title, source.Actions)                     
                    OUTPUT inserted.Id INTO @InsertedRows;
                
                SELECT Id FROM @InsertedRows;";
        
        var newTaskId  = await db.QuerySingleOrDefaultAsync<string>(sql, robotTask);

        return  string.IsNullOrEmpty(newTaskId) ? robotTask.task_id : newTaskId ;
    }

    public async Task<int> DeleteAsync(string robotTaskId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = "DELETE FROM RobotTask WHERE TaskId = @TaskId";

        return await db.ExecuteAsync(query, new { TaskId = robotTaskId });
    }
}
