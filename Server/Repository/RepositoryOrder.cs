using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.Style;
using PruebaAmaris.Server.Class;
using PruebaAmaris.Shared.Class;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static PruebaAmaris.Client.Shared.MainLayout;

namespace PruebaAmaris.Server.Repository
{
    public class RepositoryOrder : IOrder
    {
        private readonly string _connectionString;

        private readonly DapperContext _context;
        public RepositoryOrder(DapperContext context, IConfiguration configuration)
        {
            _context = context;
            
          _connectionString = configuration.GetConnectionString("AmarisConnection");

        }
        public async Task<IEnumerable<Order>> GetOrder()
        {
            var query = "SELECT * FROM Orders";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Order>(query);
                return companies.ToList();
            }
        }

        public async Task<Order> GetOrder(int id)
        {
            var query = "SELECT * FROM Orders WHERE OrderID = @Id";
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Order>(query, new { id });
                return company;
            }
        }

        public async Task<Order> CreateOrder(OrderDTO order)
        {
            var query = "INSERT INTO Orders (CustomerID, Freight, ShipCountry) VALUES (@CustomerID, @Freight, @ShipCountry)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", order.CustomerID, DbType.String);
            parameters.Add("Freight", order.Freight, DbType.String);
            parameters.Add("ShipCountry", order.ShipCountry, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdOrder = new Order
                {
                    OrderID = id,
                    CustomerID = order.CustomerID,
                    Freight = order.Freight,
                    ShipCountry = order.ShipCountry
                };
                return createdOrder;
            }
        }
 

        public async Task UpdateOrder(int id, OrderDTO order)
        {
            var query = "UPDATE Orders SET CustomerID = @CustomerID, Freight = @Freight, ShipCountry = @ShipCountry WHERE OrderID = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("CustomerID", order.CustomerID, DbType.String);
            parameters.Add("Freight", order.Freight, DbType.String);
            parameters.Add("ShipCountry", order.ShipCountry, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteOrder(int id)
        {
            var query = "DELETE FROM Orders WHERE OrderID = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }


        public void BulkInsertOrders(List<OrderData> orders)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute("INSERT INTO Orders (OrderID, CustomerID, Freight, ShipCountry) VALUES (@OrderID, @CustomerID, @Freight, @ShipCountry)", orders, transaction: transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       
                    }
                }
            }
        }


    }

}
