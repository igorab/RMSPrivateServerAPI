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

    public async Task<robot_task?> GetByTaskId(string taskId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = $@"SELECT * FROM robot_task t WHERE t.task_id = @{nameof(taskId)}";
                
        var param = new { taskId };

        var r_task = await db.QueryFirstOrDefaultAsync<robot_task>(sql, param);

        return r_task;
    }


    public async Task<robot_task?> GetCurrent(string robotId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = $@"SELECT * FROM robot_task t WHERE t.robot_id = @{nameof(robotId)}";

        var param = new { robotId };

        var car = await db.QueryFirstOrDefaultAsync<robot_task>(sql, param);

        return car;
    }


    public async Task<IEnumerable<robot_task>> GetAll(string robotId)
    {
        var builder = new SqlBuilder();
        var sqlTemplate = builder.AddTemplate(
            $@"SELECT * FROM robot_task t 
               WHERE   t.robot_id = @{nameof(robotId)}   ");
             
        using var db = _databaseConnectionFactory.GetConnection();

        return await db.QueryAsync<robot_task>(sqlTemplate.RawSql, sqlTemplate.Parameters);
    }

    public async Task<string> UpsertAsync(robot_task robotTask)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"                
                INSERT INTO robot_task (task_id, robot_id, title)
                VALUES (@task_id, @robot_id, @title)
                ON CONFLICT (task_id) DO UPDATE 
                SET                                      
                    robot_id   = EXCLUDED.robot_id,
                    title      = EXCLUDED.title                                                        
                RETURNING task_id;";
        
        var newTaskId  = await db.QuerySingleOrDefaultAsync<string>(sql, robotTask);

        return  string.IsNullOrEmpty(newTaskId) ? robotTask.task_id : newTaskId ;
    }

    public async Task<int> DeleteAsync(string robotTaskId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = "DELETE FROM robot_task WHERE task_id = @TaskId";

        return await db.ExecuteAsync(query, new { TaskId = robotTaskId });
    }
}
