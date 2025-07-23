using Dapper;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

namespace RMSPrivateServerAPI.Repositories;

public class RobotTaskRepository : IRobotTaskRepository
{
    private readonly DatabaseConnectionFactory _databaseConnectionFactory;

    public RobotTaskRepository(DatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    public async Task<RobotTask?> Get(string robotId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql =
                $@"SELECT * 
               FROM  
                    RobotTask t 
               WHERE 
                    t.robotid = @{nameof(robotId)}";
                
        var param = new { robotId };

        var car = await db.QueryFirstOrDefaultAsync<RobotTask>(sql, param);

        return car;
    }

    public async Task<IEnumerable<RobotTask>> GetAll(string robotId)
    {
        var builder = new SqlBuilder();
        var sqlTemplate = builder.AddTemplate(
            $@"SELECT * FROM RobotTask t 
               WHERE   t.robotid = @{nameof(robotId)}   ");
             
        using var db = _databaseConnectionFactory.GetConnection();

        return await db.QueryAsync<RobotTask>(sqlTemplate.RawSql, sqlTemplate.Parameters);
    }

    public async Task<int> UpsertAsync(RobotTask robotTask)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"
                DECLARE @InsertedRows AS TABLE (Id int);

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
        
        var newTaskId  = await db.QuerySingleOrDefaultAsync<int>(sql, robotTask);

        return newTaskId == 0 ? robotTask.TaskId : newTaskId ;
    }

    public async Task<int> DeleteAsync(string robotTaskId)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = "DELETE FROM RobotTask WHERE TaskId = @TaskId";

        return await db.ExecuteAsync(query, new { TaskId = robotTaskId });
    }
}
