using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.Entities.Identity;
using WebMarket.Entities.Models;

namespace WebMarket.Entities.Configurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<User>
    {
        public ApplicationUserConfiguration()
        {
            Property(x => x.Address).HasMaxLength(70);

            HasMany(u => u.Products).WithRequired(p => p.Seller).HasForeignKey(p => p.SellerId);           
            HasMany(u => u.Orders).WithRequired(o => o.Buyer).HasForeignKey(o => o.BuyerId);
        }
    }
}
