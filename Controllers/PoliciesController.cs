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
    public class PoliciesController : ControllerBase
    {
        private readonly InsureityContext _context;
        private readonly IPropertyService<Property> _propService;
        private readonly ILogger<PoliciesController> logger;

        public PoliciesController(InsureityContext context, IPropertyService<Property> propertyService, ILogger<PoliciesController> logger)
        {
            _context = context;
            _propService = propertyService;
            this.logger = logger;
        }

        // GET: api/Policies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicies()
        {
            logger.LogInformation($"Get all policies");
          if (_context.Policies == null)
          {
              return NotFound();
          }
            return await _context.Policies.Include(x=>x.PolicyMaster).Include(x=>x.Property).ToListAsync();
        }
        [HttpGet("PoliciesForBusiness")]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPoliciesForBusiness(int Bid)
        {
            logger.LogInformation($"{Bid} policies viewed");
            if (_context.Policies == null)
            {
                return NotFound();
            }
            return await _context.Policies.Include(x=>x.Property).Include(x=>x.PolicyMaster).Where(x=>x.Property.BusinessID == Bid).ToListAsync();
        }
        [HttpGet("PoliciesForAgent")]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPoliciesForAgent(int AgentId)
        {
            if (_context.Policies == null)
            {
                return NotFound();
            }
            return await _context.Policies.Include(x => x.PolicyMaster).Where(x => x.AgentId == AgentId).OrderByDescending(x=>x.CreatedAt).Take(10).ToListAsync();
        }
        [HttpGet("PoliciesForProperty")]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPoliciesForProperty(int PropertyId)
        {
            if (_context.Policies == null)
            {
                return NotFound();
            }
            return await _context.Policies.Include(x => x.PolicyMaster).Include(x => x.Property).Where(x => x.PropertyId == PropertyId).ToListAsync();
        }

        // GET: api/Policies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Policy>> GetPolicy(int id)
        {
            logger.LogInformation($"{id} Policies viewed");
          if (_context.Policies == null)
          {
              return NotFound();
          }
            var policy = await _context.Policies.Include(x=>x.PolicyMaster).Include(x=>x.Property).Where(x=>x.PolicyId==id).SingleOrDefaultAsync();

            if (policy == null)
            {
                return NotFound();
            }

            return policy;
        }


        // PUT: api/Policies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicy(int id, Policy policy)
        {
            logger.LogInformation($"{id} Policies edited");
            if (id != policy.PolicyId)
            {
                return BadRequest();
            }

            _context.Entry(policy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpGet("CreatePolicy")]
        public async Task<IActionResult> CreatePolicy(int pid)
        {
            logger.LogInformation($"New policy added");
            Property property = await _context.Properties.Include(x => x.PropertyMaster).Include(x => x.Business).Include(x => x.Business.Consumer).SingleOrDefaultAsync(x => x.PropertyId == pid);
            if (property == null)
                return NotFound();
            if (property.IsInsured)
                return BadRequest("Property is already Insured");

            Policy policy = new();
            PolicyMaster pm = await _context.PoliciesMaster.Where(x => x.PropertyType == property.PropertyMaster.PropertyType).FirstOrDefaultAsync();
            if(pm == null)
            {
                return BadRequest("No policies matching your query!");
            }
            policy.PolicyName = property.PropertyName + pm.PolicyType;
            policy.AssuredSum = pm.AssuredSum;
            policy.PremiumRate = pm.BasePremium + (double)(pm.BusinessPenalty * (10 - property.Business.BusinessScore)) + (double)(pm.PropertyPenalty * (10 - property.PropertyScore));
            policy.PremiumAmount = pm.AssuredSum * policy.PremiumRate / 100;
            policy.PolicyStatus = "Created";
            policy.PlId = pm.PlId;
            policy.PropertyId = property.PropertyId;
            policy.AgentId = property.Business.Consumer.UserId;
            _context.Policies.Add(policy);
            _context.SaveChanges();
            return Ok(policy);

        }

        [HttpPut("issuePolicy/{id}")]
        public async Task<IActionResult> IssuePolicy(int id)
        {
            logger.LogInformation($"Policy {id} is issued");
            Policy policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            policy.PolicyStatus = "Issued";
            policy.IssuedAt = DateTime.Now;
            _propService.InsureProperty(policy.PropertyId);

            _context.Entry(policy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(policy);
        }

        // POST: api/Policies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Policy>> PostPolicy(Policy policy)
        {
            logger.LogInformation($"Policy{policy.PolicyId} Added");

          if (_context.Policies == null)
          {
              return Problem("Entity set 'InsureityContext.Policies'  is null.");
          }
            try
            {
                _context.Policies.Add(policy);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPolicy", new { id = policy.PolicyId }, policy);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Policies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            logger.LogInformation($"{id} Policy Deleted");
            if (_context.Policies == null)
            {
                return NotFound();
            }
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            if(policy.IssuedAt != null)
            {
                return BadRequest("Cannot delete a policy that has already been issued!");
            }
            if(policy.QuoteId != null)
            {
                return BadRequest("Cannot delete a policy that is included in a quote!");
            }
            try
            {
                _context.Policies.Remove(policy);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        private bool PolicyExists(int id)
        {
            logger.LogInformation($"Policy {id} Status verified");
            return (_context.Policies?.Any(e => e.PolicyId == id)).GetValueOrDefault();
        }
    }
}
