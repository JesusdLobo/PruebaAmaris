using PruebaAmaris.Shared.Class;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static PruebaAmaris.Client.Shared.MainLayout;

namespace PruebaAmaris.Server.Repository
{
    public interface IOrder
    {
        public Task<IEnumerable<Order>> GetOrder();
        public Task<Order> GetOrder(int id);
        public Task<Order> CreateOrder(OrderDTO order);
        public Task UpdateOrder(int id, OrderDTO order);
        public Task DeleteOrder(int id);

        void BulkInsertOrders(List<OrderData> orders);

    }
}
