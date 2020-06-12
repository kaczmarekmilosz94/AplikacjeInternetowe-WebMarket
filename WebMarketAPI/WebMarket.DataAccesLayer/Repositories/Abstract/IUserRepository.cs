using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.Entities.Identity;
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string id);
        Task<List<User>> GetAllAsync();
        Task<List<User>> GetRangeAsync(int skip, int take);

        bool Modify(User user);
        bool Remove(User user);
        bool RemoveRange(IEnumerable<User> users);
    }
}
