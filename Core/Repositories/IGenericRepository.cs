using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
  

        Task<T> GetByIdAsync(int id);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        //Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Remove(T entitiy);

        void RemoveRange(IEnumerable<T> entities);
    }
}
