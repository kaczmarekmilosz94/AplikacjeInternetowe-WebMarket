
namespace WebMarketAPI.Models.OutputModels
{
    /// <summary>
    /// Output model for User
    /// </summary>
    public class UserOutputModel
    {
        /// <summary>
        /// Identification number of the user.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the user. Must be unique.
        /// </summary>
        public string Username { get; set; }       
        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Adress of the user.
        /// </summary>      
        public string Adress { get; set; }
        /// <summary>
        /// Phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}