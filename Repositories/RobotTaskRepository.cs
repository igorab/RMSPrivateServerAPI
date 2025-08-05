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

    public async Task<robot_task?> GetByTaskId(Guid taskId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = $@"SELECT * FROM ""RobotTask"" t WHERE t.""TaskId"" = @{nameof(taskId)}";
                
        var param = new { taskId };

        var r_task = await db.QueryFirstOrDefaultAsync<robot_task>(sql, param);

        return r_task;
    }


    public async Task<List<RobotTaskFlat?>> GetCurrent(Guid robotId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = $@"SELECT * FROM ""RobotTask"" t 
                        left join ""RobotActions"" ra 
                        on ra.""TaskId"" = t.""TaskId""
                        WHERE t.""RobotId"" = @robotId";
        
        var param = new { robotId };

        return (await db.QueryAsync<RobotTaskFlat?>(sql, param )).ToList();        
    }


    public async Task<IEnumerable<robot_task>> GetAll(Guid robot_id)
    {
        var builder = new SqlBuilder();
        var sqlTemplate = builder.AddTemplate($@"SELECT * FROM ""RobotTask"" t  WHERE t.""RobotId"" = @robot_id");
             
        using var db = _databaseConnectionFactory.GetConnection();

        return await db.QueryAsync<robot_task>(sqlTemplate.RawSql, new {robot_id} );
    }

    public async Task<Guid> UpsertAsync(robot_task robotTask)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"                
                INSERT INTO ""RobotTask"" (""TaskId"", ""RobotId"", ""Title"")
                VALUES (@TaskId, @RobotId, @Title)
                ON CONFLICT (""TaskId"") DO UPDATE 
                SET                                      
                    ""RobotId""   = EXCLUDED.""RobotId"",
                    ""Title""     = EXCLUDED.""Title""                                                        
                RETURNING ""TaskId"";";
        
        var newTaskId  = await db.QuerySingleOrDefaultAsync<Guid>(sql, robotTask);

        return  newTaskId == Guid.Empty ? robotTask.TaskId : newTaskId ;
    }

    public async Task<int> DeleteAsync(Guid robotTaskId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = @"DELETE FROM ""RobotTask"" WHERE ""TaskId"" = @TaskId";

        return await db.ExecuteAsync(query, new { TaskId = robotTaskId });
    }
}
