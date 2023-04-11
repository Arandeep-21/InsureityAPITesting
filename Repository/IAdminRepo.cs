using InsureityAPI.Controllers;
using InsureityAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsureityAPI.Repository
{
    public interface IAdminRepo<LoginDetails>
    {
        public LoginDTO VerifyAdminDetails(string email, string password);

        public void AddAdmin(LoginDetails l);
        
    }
}
