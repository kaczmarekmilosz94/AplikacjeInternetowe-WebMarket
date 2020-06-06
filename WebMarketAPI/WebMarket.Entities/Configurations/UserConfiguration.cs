using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.Entities.Models;

namespace WebMarket.Entities.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Username).HasMaxLength(20).IsRequired();
            Property(x => x.Password).HasMaxLength(20).IsRequired();
            Property(x => x.Email   ).HasMaxLength(50).IsRequired();
            Property(x => x.Adress   ).HasMaxLength(70).IsRequired();
            Property(x => x.PhoneNumber   ).HasMaxLength(12).IsRequired();

            HasMany(u => u.Products).WithRequired(p => p.Seller).HasForeignKey(p => p.SellerId);           
            HasMany(u => u.Orders).WithRequired(o => o.Buyer).HasForeignKey(o => o.BuyerId);
        }
    }
}
