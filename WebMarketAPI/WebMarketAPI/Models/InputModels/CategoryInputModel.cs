using System.ComponentModel.DataAnnotations;


namespace WebMarketAPI.Models.InputModels
{
    /// <summary>
    /// Input model for Category
    /// </summary>
    public class CategoryInputModel
    {
        /// <summary>
        /// Name of the category. Must be unique. Maximum 50 characters.
        /// </summary>
        [Required]
        [MaxLength(50)]        
        public string Name { get; set; }
    }
}