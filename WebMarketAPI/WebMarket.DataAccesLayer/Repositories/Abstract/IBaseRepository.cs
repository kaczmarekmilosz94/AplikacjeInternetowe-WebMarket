using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DataAccesLayer.Repositories.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetRangeAsync(int skip, int take);

        bool Add(TEntity entity);
        bool AddRange(IEnumerable<TEntity> entities);
        bool Modify(TEntity entity);


        bool Remove(TEntity entity);
        bool RemoveRange(IEnumerable<TEntity> entities);
    }
}
