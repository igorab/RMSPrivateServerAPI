using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using Dapper;
using RMSPrivateServerAPI.Data;

namespace RMSPrivateServerAPI.Repositories
{
    public class RobotRepository : IRobotRepository
    {
        readonly DatabaseConnectionFactory databaseConnectionFactory;

        public RobotRepository(DatabaseConnectionFactory databaseConnectionFactory)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
        }

        private async Task<T> QueryFirstOrDefaultAsync<T>(string sql, params object[] param )
        {
            using var db = databaseConnectionFactory.GetConnection();

            return await db.QueryFirstOrDefaultAsync<T>(sql, param);
        }


        public async Task<int> DeleteAsync(int id)
        {
            using var db = databaseConnectionFactory.GetConnection();

            var query = "Update RobotInfo SET is_deleted = 1 WHERE Id = @Id";

            return await db.ExecuteAsync(query,  new { Id = id });
        }

        public async Task<RobotInfo?> Get(int robotId)
        {
            var sql =
                $@"SELECT * 
                    FROM  
                RobotInfo RI 
                    WHERE 
                RI.Id = @{nameof(robotId)}
                AND RI.is_deleted = 0";

            var param = new {robotId};

            var robot = await QueryFirstOrDefaultAsync<RobotInfo>(sql, param);

            return robot;
        }

        public async Task<IEnumerable<RobotInfo>> GetAll(bool returnDeletedRecords)
        {
            var builder = new SqlBuilder();

            var sqlTemplate = builder.AddTemplate(
                "SELECT * FROM RobotInfo " +
                "/**where**/ ");

            if (!returnDeletedRecords)
            {
                builder.Where("is_deleted=0");
            }

            using var db = databaseConnectionFactory.GetConnection();

            return await db.QueryAsync<RobotInfo>(sqlTemplate.RawSql, sqlTemplate.Parameters);
        }

        public async Task<int> UpsertAsync(RobotInfo robot)
        {
            using var db = databaseConnectionFactory.GetConnection();

            var sql = @"
                DECLARE @InsertedRows AS TABLE (Id int);

                MERGE INTO RobotInfo AS target
                USING (SELECT @Id AS Id, @HardwareID AS HardwareID, @Type as Type, @Model as Model, @Name as Name, @IP as IP,
                @swVersion as swVersion, @hwVersion as hwVersion, @is_deleted AS is_deleted ) AS source 
                ON target.Id = source.Id
                WHEN MATCHED THEN 
                    UPDATE SET                         
                        HardwareID = source.HardwareID,
                        Type       = source.Type,
                        Model      = source.Model,
                        Name       = source.Name,
                        IP         = source.IP,
                        swVersion  = source.swVersion,
                        hwVersion  = source.hwVersion,
                        Is_Deleted = source.Is_Deleted
                WHEN NOT MATCHED THEN
                    INSERT (HardwareID, Type, Model, Name, IP, swVersion, hwVersion, is_deleted)
                    VALUES (source.HardwareID, source.Type, source.Model, source.Name, source.IP, source.swVersion, source.hwVersion, 
                    source.is_deleted)
                    OUTPUT inserted.Id INTO @InsertedRows
                ;

                SELECT Id FROM @InsertedRows;
            ";

            var newId = await db.QuerySingleOrDefaultAsync<int>(sql, robot);
            return newId == 0 ? robot.Id : newId;
        }
    }
}
