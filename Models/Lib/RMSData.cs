using Dapper;
using System.Data.SqlClient;
using System;
using RMSPrivateServerAPI.Entities;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Components.Web;

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

        public static bool ConnectionTest()
        {
            bool ok = false;

            if (Connect())
            {
                if (GetAllPPMTask() != null)
                {
                    ok = true;                  
                }
            }
            
            return ok; 
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


        public static List<PPMTask>? GetAllPPMTask()
        {
            try
            {
                using (var cnn = new SqlConnection(ConnectionString))
                {
                    var output = cnn.Query<PPMTask>("select * from PPMTask", new PPMTask());
                    return output.ToList();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
