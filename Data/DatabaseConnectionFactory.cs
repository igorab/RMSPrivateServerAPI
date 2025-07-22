using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;

namespace RMSPrivateServerAPI.Data;

public class DatabaseConnectionFactory
{
    readonly DbSettings _dbSettings;
    
    public DatabaseConnectionFactory(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }
    
    public IDbConnection GetConnection()
    {
        var connection = new NpgsqlConnection(_dbSettings.DefaultConnection);
        connection.Open();
        return connection;
    }
}

