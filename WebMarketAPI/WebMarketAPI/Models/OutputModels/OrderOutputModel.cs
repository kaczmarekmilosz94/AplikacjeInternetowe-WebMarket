using System;

namespace WebMarketAPI.Models.OutputModels
{
    /// <summary>
    /// Output model for Order
    /// </summary>
    public class OrderOutputModel
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
    }
}