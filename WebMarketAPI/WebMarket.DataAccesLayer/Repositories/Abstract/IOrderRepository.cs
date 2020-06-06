using System.Collections.Generic;
using System.Threading.Tasks;
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Abstract
{
    public interface IOrderRepository : IBaseRepository<Order>
    {        
        Task<List<Order>> GetOrdersOfUserAsync(int userId, int pageIndex, int pageSize);
        Task<List<Order>> GetOrdersWithProductsOfUserAsync(int userId, int pageIndex, int pageSize);
        Order GetOrderWithProducts(int id);
    }
}
