using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMarketAPI.Models.OutputModels
{
    /// <summary>
    /// Output model for Order With Products
    /// </summary>
    public class OrderWithProductsOutputModel
    {
        /// <summary>
        /// Identification number of the order.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Time of the purchase.
        /// </summary>        
        public DateTime PurchaseTime { get; set; }
        /// <summary>
        /// Identification number of user the order was made by.
        /// </summary>
        public string BuyerId { get; set; }
        /// <summary>
        /// Collection containing all products of given order
        /// </summary>
        public List<ProductOutputModel> Products { get; set; }
    }
}