using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Order> GetByCustomerAndShopAsync(int basketId, string userId)
        {
            IQueryable<Order> query = _context.Orders.AsNoTracking()
                .Include(i => i.OrderItems)
                .ThenInclude(p => p.Product);

            query = query.Where(o => o.BasketId == basketId
                                  && o.UserId == userId);

            var order = await query.FirstOrDefaultAsync();
            if (order != null)
            {
                order.Total = order.OrderItems.Sum(x => x.Product.Price * x.Quantity);
            }
            return order;
        }

        public async Task<List<Order>> GetOrders(string userId)
        {
            var orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).AsQueryable();

            if (!string.IsNullOrEmpty(userId.ToString()))
            {
                orders = orders.Where(x => x.UserId == userId);
            }

            return await orders.ToListAsync();
        }
    }
}
