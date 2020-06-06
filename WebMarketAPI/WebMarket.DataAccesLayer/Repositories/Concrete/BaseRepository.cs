using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataAccesLayer.Repositories.Abstract;
using WebMarket.Entities;

namespace WebMarket.DataAccesLayer.Repositories.Concrete
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext appContext;
        public BaseRepository(AppDbContext context)
        {
            appContext = context;
        }


        public bool Add(TEntity entity)
        {
            if (entity == null)
                return false;

            try
            {
                appContext.Set<TEntity>().Add(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }       
        public bool AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return false;

            try
            {
                appContext.Set<TEntity>().AddRange(entities);
            }
            catch
            {
                return false;
            }

            return true;
        }
        public bool Modify(TEntity entity)
        {
            if (entity == null)
                return false;

            try
            {
                appContext.Entry<TEntity>(entity).State = EntityState.Modified;
            }
            catch
            {
                return false;
            }

            return true;
        }


        public async Task<TEntity> GetAsync(int id)
        {
            return await appContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await appContext.Set<TEntity>().ToListAsync();
        }
        public async Task<List<TEntity>> GetRangeAsync(int pageIndex, int pageSize)
        {
            return await appContext.Set<TEntity>()
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public bool Remove(TEntity entity)
        {
            if (entity == null)
                return false;

            try
            {
                appContext.Set<TEntity>().Remove(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }
        public bool RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return false;

            try
            {
                appContext.Set<TEntity>().RemoveRange(entities);
            }
            catch
            {
                return false;
            }

            return true;
        }      
    }
}
