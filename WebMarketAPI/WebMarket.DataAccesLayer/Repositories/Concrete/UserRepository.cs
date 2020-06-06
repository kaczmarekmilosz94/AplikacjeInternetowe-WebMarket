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
using WebMarket.Entities.Models;

namespace WebMarket.DataAccesLayer.Repositories.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
