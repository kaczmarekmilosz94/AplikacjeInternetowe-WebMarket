using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataAccesLayer.Repositories.Abstract;
using WebMarket.Entities;
using WebMarket.Entities.Identity;
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appContext;
        public UserRepository(AppDbContext context)
        {
            _appContext = context;
        }


        public async Task<List<User>> GetAllAsync()
        {
            var users = await _appContext.Users.ToListAsync();
            return users;
        }
        public async Task<User> GetAsync(string id)
        {
            var user = await _appContext.Set<User>().FindAsync(id);
            return user;
        }
        public async Task<List<User>> GetRangeAsync(int skip, int take)
        {
            var users = await _appContext.Users
                .OrderBy(x => x.UserName)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return users;
        }

        public bool Modify(User user)
        {
            if (user == null)
                return false;

            try
            {
                _appContext.Entry<User>(user).State = EntityState.Modified;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Remove(User user)
        {
            if (user == null)
                return false;

            try
            {
                _appContext.Users.Remove(user);
            }
            catch
            {
                return false;
            }

            return true;          
        }
        public bool RemoveRange(IEnumerable<User> users)
        {
            if (users == null)
                return false;

            try
            {
                _appContext.Set<User>().RemoveRange(users);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
