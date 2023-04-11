using System.ComponentModel.DataAnnotations;

namespace InsureityAPI.Models
{
    public class LoginDetails
    {
        [Key] 
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        [Required]
        public string? UserPassword { get; set; }
        public byte[]? UserSalt { get; set; }
        [Required]
        public string? Role { get; set;}
        public ICollection<Policy>? Policies { get; set; }
    }
}
