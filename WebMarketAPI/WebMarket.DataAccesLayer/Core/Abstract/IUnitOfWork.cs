using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataAccesLayer.Repositories.Abstract;

namespace WebMarket.DataAccesLayer.Core.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        IUserRepository Users { get; }

        Task<int> Commit();
    }
}
