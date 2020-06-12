using System;
using System.Collections.Generic;
using WebMarket.Entities.Identity;

namespace WebMarket.Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime PurchaseTime { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string BuyerId { get; set; }
        public virtual User Buyer { get; set; }
    }
}
