using InsureityAPI.Models;
using InsureityAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo<LoginDetails> adminRepo;
        private readonly IAgentRepo<LoginDetails> agentRepo;
        private readonly AuthController ac;
        private readonly ILogger<AdminController> logger;
        public AdminController(IAdminRepo<LoginDetails> objs, IAgentRepo<LoginDetails> obj, AuthController _ac, ILogger<AdminController> _logger)
        {
            adminRepo = objs;
            agentRepo = obj;
            ac = _ac;
            logger = _logger;
        }

        [AllowAnonymous]
        [HttpPost("AdminAuth")]
        public ActionResult<LoginResponse> VerifyAdminDetails(LoginDTO l)
        {
            logger.LogInformation("Admin Details verification");
            try
            {
                var prods = adminRepo.VerifyAdminDetails(l.UserEmail, l.UserPassword);
                if (prods == null) { 
                    return Ok(1);
                }
                return Ok(ac.Login(prods));
            }
            catch
            {
                return Unauthorized();
            }
            
        }

        [HttpPost]
        [Route("Admin")]
        public async Task<ActionResult> AddAdmin(LoginDetails l)
        {
            logger.LogInformation($"Admin {l.UserEmail}");
            try
            {
                if (ModelState.IsValid)
                {
                    l.Role = "Admin";
                    adminRepo.AddAdmin(l);
                    return Ok();
                }
                return Ok(0);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        

    }
}
