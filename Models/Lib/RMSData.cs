using Dapper;
using RMSPrivateServerAPI.Entities;
using Npgsql;

namespace RMSPrivateServerAPI.Models.Lib
{
    public static class RMSData
    {
        private static string? _connectionString ;

        public static string? ConnectionString
        {
            set { _connectionString = value; }
            get { return _connectionString; }
        }

        static RMSData()
        {
            _connectionString = WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection");
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
                using (var connection = new NpgsqlConnection(_connectionString))
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


        public static List<ppmtask>? GetAllPPMTask()
        {
            try
            {
                using (var cnn = new NpgsqlConnection(ConnectionString))
                {
                    var output = cnn.Query<ppmtask>(@"select * from ""PPMTask""", new ppmtask());
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
