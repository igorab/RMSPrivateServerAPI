using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

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
        var connection = new SqlConnection(_dbSettings.DefaultConnection);
        connection.Open();
        return connection;
    }
}

