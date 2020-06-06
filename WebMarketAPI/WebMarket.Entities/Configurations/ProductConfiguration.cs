using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.Entities.Models;

namespace WebMarket.Entities.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(50);

            HasRequired(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            HasRequired(p => p.Seller).WithMany(s => s.Products).HasForeignKey(p => p.SellerId);
            HasOptional(p => p.Order).WithMany(o => o.Products).HasForeignKey(p => p.OrderId);
        }
    }
}
