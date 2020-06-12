using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebMarket.Entities.Identity;

namespace WebMarket.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string ImageURL { get; set; }       


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string SellerId { get; set; }
        public virtual User Seller { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public virtual ICollection<Basket> Baskets { get; set; }
    }
}
    