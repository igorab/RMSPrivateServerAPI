using Dapper;
using Microsoft.Data.SqlClient;
using System;

namespace RMSPrivateServerAPI.Models.Lib
{
    public class RMSData
    {
        private static string? _connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = RMSdb; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";

        public static string? ConnectionString
        {
            set { _connectionString = value; }
            get { return _connectionString; }
        } 

        public static bool Connect()
        {
            bool ok = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                ok = false;
            }

            return ok;
        }


        public static List<APRStatus> LoadAPRStatus()
        {
            try
            {
                using (var cnn = new SqlConnection(ConnectionString))
                {
                    var output = cnn.Query<APRStatus>("select * from APRStatus", new APRStatus());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<APRStatus>();
            }
        }


    }
}
