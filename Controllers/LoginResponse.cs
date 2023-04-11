namespace InsureityAPI.Controllers
{
    public class LoginResponse
    {
        public string token { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string? Role { get; set; }
    }
}
