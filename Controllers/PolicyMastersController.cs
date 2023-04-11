using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsureityAPI.Models;
using InsureityAPI.Services;

namespace InsureityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyMastersController : ControllerBase
    {
        private readonly IPolicyMasterService<PolicyMaster> _policymasterService;
        private readonly ILogger<PolicyMastersController> logger;
        public PolicyMastersController(IPolicyMasterService<PolicyMaster> policymasterService, ILogger<PolicyMastersController> logger)
        {
            _policymasterService = policymasterService;
            this.logger = logger;
        }

        // GET: api/PolicyMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolicyMaster>>> GetPoliciesMaster()
        {
            logger.LogInformation($"Get all policies");
            return _policymasterService.GetAllPolicyMaster();
        }

        // GET: api/PolicyMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PolicyMaster>> GetPolicyMaster(int id)
        {
            logger.LogInformation($"Policy {id} viewed");
            var policyMaster = _policymasterService.ViewPolicyMaster(id);

            if (policyMaster == null)
            {
                return NotFound();
            }

            return policyMaster;
        }

        // PUT: api/PolicyMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicyMaster(int id, PolicyMaster policyMaster)
        {
            logger.LogInformation($"Policy {id} updated");
            if (id != policyMaster.PlId)
            {
                return BadRequest();
            }

            try
            {
                _policymasterService.UpdatePolicyMaster(id, policyMaster);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/PolicyMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PolicyMaster>> PostPolicyMaster(PolicyMaster policyMaster)
        {
            logger.LogInformation($"New Policy Master Created");
            _policymasterService.AddPolicyMaster(policyMaster);

            return CreatedAtAction("GetPolicyMaster", new { id = policyMaster.PlId }, policyMaster);
        }

        // DELETE: api/PolicyMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicyMaster(int id)
        {
            logger.LogInformation($"Policy {id} deleted");
            _policymasterService.DeletePolicyMaster(id);
            return NoContent();
        }

    }
}
