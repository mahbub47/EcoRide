using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace EcoRide.Data.DbConnecction
{
    public class SqlDbConnection
    {
        //private static readonly Lazy<SqlDbConnection> _instance = new(() => new SqlDbConnection());
        //public static SqlDbConnection Instance => _instance.Value;
        public string ConnectionString { get; }

        public SqlDbConnection()
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public SqlConnection Connect()
        {
            return new SqlConnection(ConnectionString);
        }

    }
}
