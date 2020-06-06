

namespace WebMarketAPI.Models.OutputModels
{
    /// <summary>
    /// Output model for Product
    /// </summary>
    public class ProductOutputModel
    {
        /// <summary>
        /// Identification number of the product.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Name of category the product belongs to.
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Username of product seller.
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// Identification number of the order the product is part of. NULL if product is not sold yet.
        /// </summary>
        public int? OrderId { get; set; }
    }
}