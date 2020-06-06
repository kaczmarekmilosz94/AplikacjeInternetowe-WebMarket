using System.Collections.Generic;
using System.Threading.Tasks;
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Abstract
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetProductsOfCategoryAsync(int categoryId, int pageIndex, int pageSize);     
        Task<List<Product>> GetOfferedProductsOfUserAsync(int userId, int pageIndex, int pageSize);
        Task<List<Product>> GetSoldProductsOfUserAsync(int userId, int pageIndex, int pageSize);
        List<Product> GetProductsFromBasketOfUser(int userId, int pageIndex, int pageSize);
    }
}
