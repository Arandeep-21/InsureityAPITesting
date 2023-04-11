using InsureityAPI.Models;
using InsureityAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService<Property> PropertyService;
        private readonly ILogger<PropertiesController> logger;
        public PropertiesController(IPropertyService<Property> _PropertyService, ILogger<PropertiesController> logger)
        {
            PropertyService = _PropertyService;
            this.logger = logger;
        }

        [HttpGet("id")]
        public async Task<ActionResult<Property>> ViewBusinessProperty(int id)
        {
            logger.LogInformation($"Property {id} viewed");
            var property = PropertyService.ViewProperty(id);

            if(property == null)
            {
                return NotFound();
            }

            return property;
        }

        [HttpPost]
        public async Task<ActionResult<Property>> CreateBusinessProperty(Property property)
        {
            logger.LogInformation($"Property {property.PropertyId} Created");
            try
            {
                PropertyService.AddProperty(property);
            }
            catch (DbUpdateException) {
                if (PropertyExists(property.PropertyId))
                    return Conflict();
                else
                    throw;
            }

            return CreatedAtAction("ViewBusinessProperty", new { id = property.PropertyId }, property);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateBusinessProperty(int id, Property property)
        {
            logger.LogInformation($"Property {id} updated");
            if(id != property.PropertyId)
                return BadRequest();

            try
            {
                PropertyService.UpdateProperty(id, property);
            } catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        private bool PropertyExists(int id)
        {
            logger.LogInformation($"Property {id} status");
            if (PropertyService.ViewProperty(id) == null)
                return false;

            return true;
        }

        [HttpGet("ViewBusinessProperty")]
        public async Task<ActionResult<IEnumerable<Property>>> GetPropertiesForBusiness(int BusinessId)
        {
            logger.LogInformation($"Get all properties for Business {BusinessId}");
            var property = PropertyService.GetPropertiesForBusiness(BusinessId);

            if (property == null)
            {
                return NotFound();
            }

            return property;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetAllProperties()
        {
            logger.LogInformation($"Get all properties");
            var property = PropertyService.GetAllProperties();
            return property;
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteProperty(int id)
        {
            logger.LogInformation($"Property {id} deleted");
            var property = PropertyService.ViewProperty(id);
            if (property == null)
            {
                return NotFound();
            }

            PropertyService.DeleteProperty(id);

            return NoContent();
        }


    }
}
