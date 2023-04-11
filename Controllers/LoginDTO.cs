using System.ComponentModel.DataAnnotations;

namespace InsureityAPI.Controllers
{
    public class LoginDTO
    {
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public byte[]? UserSalt { get; set; }
        public string? Role { get; set; }
    }
}
