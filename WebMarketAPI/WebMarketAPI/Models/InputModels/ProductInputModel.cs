using System.ComponentModel.DataAnnotations;


namespace WebMarketAPI.Models.InputModels
{
    /// <summary>
    /// Input model for Product
    /// </summary>
    public class ProductInputModel
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }
        /// <summary>
        /// Identification number of category the product belongs to.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
        /// <summary>
        /// Identification number of user that sells the product.
        /// </summary>
        [Required]
        public int SellerId { get; set; }
    }
}