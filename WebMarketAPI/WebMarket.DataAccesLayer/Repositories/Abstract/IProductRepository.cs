using System.Collections.Generic;
using System.Threading.Tasks;
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Abstract
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetProductsOfCategoryAsync(int categoryId, int pageIndex, int pageSize);     
        Task<List<Product>> GetOfferedProductsOfUserAsync(string userId, int pageIndex, int pageSize);
        Task<List<Product>> GetSoldProductsOfUserAsync(string userId, int pageIndex, int pageSize);
        List<Product> GetProductsFromBasketOfUser(string userId, int pageIndex, int pageSize);
    }
}
