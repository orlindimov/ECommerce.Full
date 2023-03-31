using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<List<Order>> GetOrders(string userId);

        Task<Order> GetByCustomerAndShopAsync(int basketId, string userId);
    }
}
