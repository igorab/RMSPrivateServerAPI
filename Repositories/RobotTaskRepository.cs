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

    public async Task<robot_task?> Get(string taskId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql =
                $@"SELECT * 
               FROM  
                    RobotTask t 
               WHERE 
                    t.robotid = @{nameof(taskId)}";
                
        var param = new { taskId };

        var car = await db.QueryFirstOrDefaultAsync<robot_task>(sql, param);

        return car;
    }


    public async Task<robot_task?> GetCurrent(string robotId)
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

                MERGE INTO robot_task AS target
                USING (SELECT @robot_id AS RobotId, @task_id AS TaskId, @title as Title) AS source                 
                ON target.Id = source.Id
                WHEN MATCHED THEN 
                    UPDATE SET                         
                        robotId   = source.robot_id,
                        title     = source.title,                        
                WHEN NOT MATCHED THEN
                    INSERT (robot_id, title)
                    VALUES (source.RobotId, source.Title)                     
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
