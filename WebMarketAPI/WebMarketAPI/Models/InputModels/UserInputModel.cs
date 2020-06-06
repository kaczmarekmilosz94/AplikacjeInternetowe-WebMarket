using System.ComponentModel.DataAnnotations;


namespace WebMarketAPI.Models.InputModels
{
    /// <summary>
    /// Input model for User
    /// </summary>
    public class UserInputModel
    {
        /// <summary>
        /// Name of the user. Must be unique.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }       
        /// <summary>
        /// Password of the user.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        /// <summary>
        /// Email of the user.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Not valid e-mail address.")]
        public string Email { get; set; }
        /// <summary>
        /// Adress of the user.
        /// </summary>
        [Required]
        [MaxLength(70)]
        public string Adress { get; set; }
        /// <summary>
        /// Phone number of the user.
        /// </summary>
        [Required]
        [MinLength(7)]
        [MaxLength(12)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Not valid phone number.")]
        public string PhoneNumber { get; set; }
    }
}