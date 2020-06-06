using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Basket> Baskets { get; set; }


        public int SellerId { get; set; }
        public virtual User Seller { get; set; }
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
    