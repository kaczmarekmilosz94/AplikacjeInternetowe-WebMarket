using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.Entities.Models;

namespace WebMarket.Entities.Configurations
{
    public class BasketConfiguration : EntityTypeConfiguration<Basket>
    {
        public BasketConfiguration()
        {
            HasMany(b => b.Products).WithMany(p => p.Baskets);
            HasRequired(b => b.Owner).WithRequiredDependent(o => o.Basket);
            HasKey(b => b.OwnerId);
        }
    }
}
