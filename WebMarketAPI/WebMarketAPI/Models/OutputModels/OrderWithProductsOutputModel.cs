using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMarketAPI.Models.OutputModels
{
    /// <summary>
    /// Output model for Order With Products
    /// </summary>
    public class OrderWithProductsOutputModel : OrderOutputModel
    {
        /// <summary>
        /// Collection containing all products of given order
        /// </summary>
        public List<ProductOutputModel> Products { get; set; }
    }
}