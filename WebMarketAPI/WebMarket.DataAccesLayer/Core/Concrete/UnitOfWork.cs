using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataAccesLayer.Core.Abstract;
using WebMarket.DataAccesLayer.Repositories.Abstract;
using WebMarket.DataAccesLayer.Repositories.Concrete;
using WebMarket.Entities;

namespace WebMarket.DataAccesLayer.Core.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {        
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Orders = new OrderRepository(_context);
            Users = new UserRepository(_context);
        }


        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IUserRepository Users { get; private set; }


        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }
         
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
