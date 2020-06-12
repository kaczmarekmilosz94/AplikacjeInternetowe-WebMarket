using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.DataAccesLayer.Repositories.Abstract;
using WebMarket.Entities;
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Concrete
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsOfCategoryAsync(int categoryId, int pageIndex, int pageSize)
        {
            return await appContext.Products
                .Where(p => p.CategoryId == categoryId)
                .OrderBy(p => p.Name)
                .Skip((pageIndex * pageSize))
                .Take(pageSize)
                .ToListAsync();
        }      
        public async Task<List<Product>> GetOfferedProductsOfUserAsync(string sellerId, int pageIndex, int pageSize)
        {
            return await appContext.Products
                .Where(p => p.SellerId == sellerId)
                .OrderBy(p => p.Name)
                .Skip((pageIndex * pageSize))
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Product>> GetSoldProductsOfUserAsync(string userId, int pageIndex, int pageSize)
        {
            return await appContext.Products
               .Where(p => p.SellerId == userId && p.Order != null)
               .OrderBy(p => p.Order.PurchaseTime)
               .Skip((pageIndex * pageSize))
               .Take(pageSize)
               .ToListAsync();
        }
        public List<Product> GetProductsFromBasketOfUser(string userId, int pageIndex, int pageSize)
        {
            return appContext.Users.Where(u => u.Id == userId)
                .FirstOrDefault()
                ?.Basket.Products
                .OrderBy(p => p.Name)
                .Skip((pageIndex * pageSize))
                .Take(pageSize).ToList();
        }
    }
}
