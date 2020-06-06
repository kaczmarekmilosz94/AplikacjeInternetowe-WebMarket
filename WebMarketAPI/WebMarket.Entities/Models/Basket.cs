using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.Entities.Models
{
    public class Basket
    { 
        [Key][ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
