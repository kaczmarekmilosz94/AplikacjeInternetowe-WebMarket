using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.DataAccesLayer.Repositories.Abstract;
using WebMarket.Entities;
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Concrete
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Order>> GetOrdersOfUserAsync(string userId, int pageIndex, int pageSize)
        {
            return await appContext.Orders
                .Where(o => o.BuyerId == userId)
                .OrderBy(o => o.PurchaseTime)
                .Skip((pageIndex * pageSize))
                .Take(pageSize)
                .ToListAsync();
        }       
        public async Task<List<Order>> GetOrdersWithProductsOfUserAsync(string userId, int pageIndex, int pageSize)
        {
            return await appContext.Orders
                 .Where(o => o.BuyerId == userId)
                 .Include(o => o.Products)
                 .OrderBy(o => o.PurchaseTime)
                 .Skip((pageIndex * pageSize))
                 .Take(pageSize)
                 .ToListAsync();
        }
        public Order GetOrderWithProducts(int id)
        {
            return appContext.Orders
                 .Where(o => o.Id == id)
                 .Include(o => o.Products)
                 .FirstOrDefault();
        }
    }
}
