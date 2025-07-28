using Dapper;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

namespace RMSPrivateServerAPI.Repositories;

public class PPMRepository : IPPMRepository
{
    private readonly DatabaseConnectionFactory _databaseConnectionFactory;

    public PPMRepository(DatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }
    
    public async Task<IEnumerable<ppmtask>> GetAll()
    {
        var builder = new SqlBuilder();

        var sqlTemplate = builder.AddTemplate(
            "SELECT * FROM ppmtask " 
            );
        
        using var db = _databaseConnectionFactory.GetConnection();

        return await db.QueryAsync<ppmtask>(sqlTemplate.RawSql, sqlTemplate.Parameters);
    }

    public async Task<ppmtask?> Get(int id)
    {
        var sql =
                $@"SELECT * 
                    FROM  
                PPMTask  T 
                    WHERE 
                T.Id = @{nameof(id)}";

        var param = new {id};

        using var db = _databaseConnectionFactory.GetConnection();

        var ppmTask = await db.QueryFirstOrDefaultAsync<ppmtask>(sql, param);

        return ppmTask;
    }


    public async Task<int> UpsertAsync(ppmtask ppmTask)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var sql = @"
                DECLARE @InsertedRows AS TABLE (Id int);

                MERGE INTO PPMTask AS target
                USING (SELECT @RobotId AS RobotId, @TaskDescription AS TaskDescription, @ScheduledDate as ScheduledDate) AS source                 
                ON target.Id = source.Id
                WHEN MATCHED THEN 
                    UPDATE SET                         
                        RobotId          = source.RobotId,
                        TaskDescriptiopn = source.TaskDescription,
                        ScheduledDate    = source.ScheduledDate                        
                WHEN NOT MATCHED THEN
                    INSERT (RobotId, TaskDescription, ScheduledDate)
                    VALUES (source.RobotId, source.TaskDescription, source.ScheduledDate)                     
                    OUTPUT inserted.Id INTO @InsertedRows;
                
                SELECT Id FROM @InsertedRows;";

        var newTaskId = await db.QuerySingleOrDefaultAsync<int>(sql, ppmTask);

        return newTaskId == 0 ? ppmTask.id : newTaskId;
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var db = _databaseConnectionFactory.GetConnection();

        var query = @"DELETE FROM PPMTask WHERE Id = @id";

        return await db.ExecuteAsync(query, new { Id = id }); 
    }
}
