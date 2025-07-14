using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace RMSPrivateServerAPI.Data;

public class DatabaseConnectionFactory
{
    readonly DbSettings dbSettings;
    
    public DatabaseConnectionFactory(IOptions<DbSettings> dbSettings)
    {
        this.dbSettings = dbSettings.Value;
    }
    
    public IDbConnection GetConnection()
    {
        var connection = new SqlConnection(dbSettings.DefaultConnection);
        connection.Open();
        return connection;
    }
}

