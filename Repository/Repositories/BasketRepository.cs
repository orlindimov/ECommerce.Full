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
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext context) : base(context)
        {
        }

        public void DeleteFromBasket(int Id)
        {
            var basketItem = _context.BasketItems.FirstOrDefault(b => b.ProductId == Id);

            _context.Remove(basketItem);
            basketItem.Quantity--;
       
        }

        public async Task<Basket> GetBasket(string userId)
        {
            return _context.Baskets.Include(x => x.BasketItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);
        }

        public async Task<Basket> SaveOrUpdate(Basket basket)
        {
            var basketItem = await _context.Baskets.Include(x => x.BasketItems).SingleOrDefaultAsync(x => x.Id == basket.Id);

            return basketItem;
        }

      
    }
}
