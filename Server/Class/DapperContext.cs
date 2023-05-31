using Microsoft.Data.SqlClient;
using System.Data;

namespace PruebaAmaris.Server.Class
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AmarisConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
