using System.Collections.Generic;
using System.Threading.Tasks;
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Abstract
{
    public interface IOrderRepository : IBaseRepository<Order>
    {        
        Task<List<Order>> GetOrdersOfUserAsync(string userId, int pageIndex, int pageSize);
        Task<List<Order>> GetOrdersWithProductsOfUserAsync(string userId, int pageIndex, int pageSize);
        Order GetOrderWithProducts(int id);
    }
}
