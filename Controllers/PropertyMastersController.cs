using InsureityAPI.Models;
using InsureityAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace InsureityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyMastersController : Controller
    {
        private readonly IUnitOfWork _uw;
        private readonly ILogger<PropertyMastersController> logger;
        private readonly InsureityContext _context;
        public PropertyMastersController(IUnitOfWork uw, ILogger<PropertyMastersController> logger, InsureityContext context)
        {
            _uw = uw;
            _context = context;
            this.logger = logger;
        }


        [HttpGet]
        [Route("/PM/GetAll")]
        public async Task<ActionResult<List<PropertyMaster>>> GetAllPropertyMasters()
        {
            logger.LogInformation($"Get all Permissible Properties");
            return await _uw.PropertyMasters.GetAllAsync();
        }

        [HttpGet("/PM/GetById")]
        public async Task<ActionResult<PropertyMaster>> GetPropertyMaster(int id)
        {
            logger.LogInformation($"Property Master {id} viewed");
            return await _uw.PropertyMasters.GetAsync(x => x.PMId == id);
        }


        [HttpPost]
        [Route("/PM/Create")]
        public async Task<ActionResult> CreatePropertyMaster(PropertyMaster b)
        {
            logger.LogInformation($"{b.PMId} Permissible Property added");
            await _uw.PropertyMasters.CreateAsync(b);
            return Ok();
        }


        [HttpDelete]
        [Route("/PM/Remove")]
        public async Task<ActionResult> RemovePropertyMaster(int id)
        {
            logger.LogInformation($"Property {id} removed");
            PropertyMaster pm = await _uw.PropertyMasters.GetAsync(x=>x.PMId == id);
            if(pm == null)
            {
                return NotFound();
            }
            await _uw.PropertyMasters.RemoveAsync(pm);
            return Ok();
        }

        [HttpPut]
        [Route("/PM/Update")]
        public async Task<ActionResult> UpdatePropertyMaster(int id, PropertyMaster b)
        {
            logger.LogInformation($"Permissible Property {id} updated");
            if(id != b.PMId)
            {
                return BadRequest();
            }
            PropertyMaster p = await _uw.PropertyMasters.GetAsync(x=>x.PMId == id);

            if(p == null)
            {
                return NotFound();
            }
            p.PropertyType = b.PropertyType;
            await _uw.PropertyMasters.UpdateAsync(p);
            return Ok();
        }



        [HttpGet("{type}")]
        public IActionResult ChecktypeExists(string type)
        {
            bool typeExists = Lookuptype(type);
            return Ok(new { typeExists });
        }

        private bool Lookuptype(string type)
        {
            var ans = _context.PropertyMaster.Where(x => x.PropertyType == type).ToList();
            if (!ans.Any())
            {
                return false;
            }
            return true;
        }
    }
}
