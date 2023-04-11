using InsureityAPI.Models;
using InsureityAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : Controller
    {
        private readonly IBusinessService<Business> _businessService;
        private readonly ILogger<BusinessController> logger;

        public BusinessController(IBusinessService<Business> businessService, ILogger<BusinessController> logger)
        {
            _businessService = businessService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusiness()
        {
            logger.LogInformation("Get all Businesses");
            var res = _businessService.GetAllBusiness();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> GetBusiness(int id)
        {
            logger.LogInformation($"{id} Business viewed");
            var business = _businessService.ViewBusiness(id);

            if (business == null)
            {
                return NotFound();
            }

            return business;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusiness(int id, Business b)
        {
            logger.LogInformation($"Business {id} was edited");
            
            if (id != b.BusinessId)
            {
                return BadRequest();
            }

            try
            {
                _businessService.UpdateBusiness(id, b);

            }
            catch (DbUpdateConcurrencyException)
            {
               
                if (!BusinessExists(id))
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
        [HttpPost]
        public async Task<ActionResult<Business>> PostBusiness(Business b)
        {
            logger.LogInformation($"Business Added");
            try
            {
                _businessService.AddBusiness(b);
                return Ok(b);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpGet("ViewConsumerBusiness")]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinessForConsumer(int ConsumerId)
        {
            logger.LogInformation($"Getting all Businesses under {ConsumerId}");
            var business = _businessService.GetBusinessForConsumer(ConsumerId);

            if (business == null)
            {
                return NotFound();
            }

            return business;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            logger.LogInformation($"{id} business deleted");
            var business = _businessService.ViewBusiness(id);
            if (business == null)
            {
                return NotFound();
            }
            try
            {
                _businessService.DeleteBusiness(business);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            

            return NoContent();
        }

        private bool BusinessExists(int id)
        {
            logger.LogInformation($"Business exists {id}");
            var b = _businessService.ViewBusiness(id);
            if (b == null)
            {
                return false;
            }
            else
            {
                return true;

            }
        }
    }
}

