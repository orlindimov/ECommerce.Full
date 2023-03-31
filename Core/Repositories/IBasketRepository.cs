using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IBasketRepository:IGenericRepository<Basket>
    {
        Task<Basket> GetBasket(string userId);

         void DeleteFromBasket(int id);

        Task<Basket> SaveOrUpdate(Basket basket);

        
    }
}
