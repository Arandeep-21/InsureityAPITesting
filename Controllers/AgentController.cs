using InsureityAPI.Models;
using InsureityAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace InsureityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepo<LoginDetails> agentRepo;
        private readonly AuthController ac;
        private readonly InsureityContext context;
        private readonly ILogger<AgentController> logger;
        public AgentController(IAgentRepo<LoginDetails> objs,AuthController _ac, InsureityContext context, ILogger<AgentController> logger)
        {
            agentRepo = objs;
            ac = _ac;
            this.context = context;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("AgentAuth")]
        public ActionResult<LoginResponse> VerifyAgentDetails(LoginDTO l)
        {
            logger.LogInformation("Agent Verification");
            try
            {
                var prods = agentRepo.VerifyAgentDetails(l.UserEmail, l.UserPassword);
                return Ok(ac.Login(prods));
            }
            catch
            {
                return Unauthorized();
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddAgent(LoginDetails l)
        {
            logger.LogInformation($"Add agent");
            try
            {
                    l.Role = "Agent";
                    agentRepo.AddAgent(l);
                    return Ok(1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAgent(int id, LoginDetails l)
        {
            logger.LogInformation($"Updated agent {id}");
            try
            {
                if (ModelState.IsValid)
                {
                    agentRepo.UpdateAgent(l);
                    return Ok();
                }
                return Ok(0);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]

        public ActionResult DeleteAgent(int lid)
        {
            logger.LogInformation($"Deleted {lid}");
            try
            {
                LoginDetails ld = agentRepo.GetAgent(lid);
                agentRepo.DeleteAgent(ld);
                return Ok(1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<LoginDetails> GetAgent(int agentId)
        {
            logger.LogInformation($"Viewed {agentId}");
            try
            {
                return Ok(agentRepo.GetAgent(agentId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetByEmail")]
        public ActionResult<LoginDetails> GetAgentByEmail(string email)
        {
            try
            {
                return Ok(agentRepo.GetAllAgents().Where(x=>x.UserEmail == email).SingleOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<LoginDetails>> GetAllAgents()
        {
            logger.LogInformation($"Get all Agents");
            try
            {
                return Ok(agentRepo.GetAllAgents());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAgentPolicies")]
        public ActionResult<IEnumerable<Policy>> GetAgentPolicies(int agentId)
        {
            logger.LogInformation($"{agentId} Policies");
            try
            {
                return Ok(context.Policies.Where(x=>x.AgentId==agentId).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{email}")]
        public IActionResult CheckEmailExists(string email){
            if (!IsValidEmail(email))
            {
                return BadRequest("Invalid email address");
            }
            bool emailExists = LookupEmail(email);
            return Ok(new { emailExists });
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool LookupEmail(string email)
        {
            var dbb = context.LoginDetailsList.Where(x => x.UserEmail == email && x.Role == "Agent").ToList();
            if (!dbb.Any()) {
                return false;
            }
            return true;
        }
    }
}
