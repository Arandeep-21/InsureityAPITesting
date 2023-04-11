using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsureityAPI.Models;
using InsureityAPI.Repository;
using Microsoft.Extensions.Options;

namespace InsureityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessMastersController : Controller
    {
        private readonly IUnitOfWork _uw;
        private readonly ILogger<BusinessMastersController> logger;

        private readonly InsureityContext _context;
        public BusinessMastersController(IUnitOfWork uw,InsureityContext context, ILogger<BusinessMastersController> logger)
        {
            _context = context;
            _uw = uw;
            this.logger = logger;
        }


        [HttpGet]
        [Route("/BM/GetAll")]
        public async Task<ActionResult<List<BusinessMaster>>> GetAllBusinessMasters()
        {
            logger.LogInformation("Getting all business master elements");
            return await _uw.BusinessMasters.GetAllAsync();
        }

        [HttpGet("/BM/GetById")]
        public async Task<ActionResult<BusinessMaster>> GetBusinessMaster(int id)
        {
            logger.LogInformation($"Id {id} permissible business viewed");
            return await _uw.BusinessMasters.GetAsync(x => x.BMId == id);
        }


        [HttpPost]
        [Route("/BM/Create")]
        public async Task<ActionResult> CreateBusinessMaster(BusinessMaster b)
        {
            logger.LogInformation($"{b.BMId} added");
            await _uw.BusinessMasters.CreateAsync(b);
            return Ok();
        }


        [HttpPost]
        [Route("/BM/Remove")]
        public async Task<ActionResult> RemoveBusinessMaster(int id)
        {
            logger.LogInformation($"{id} removed");
            BusinessMaster bm = await _uw.BusinessMasters.GetAsync(x => x.BMId == id);
            await _uw.BusinessMasters.RemoveAsync(bm);
            BusinessMaster pm = await _uw.BusinessMasters.GetAsync(x => x.BMId == id);
            if (pm == null)
            {
                return NotFound();
            }
            await _uw.BusinessMasters.RemoveAsync(pm);
            return Ok();
        }

        [HttpPost]
        [Route("/BM/Update")]
        public async Task<ActionResult> UpdateBusinessMaster(BusinessMaster b)
        {
            logger.LogInformation($"{b.BMId} updated");
            await _uw.BusinessMasters.UpdateAsync(b);
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
           
                var ans = _context.BusinessMaster.Where(x => x.BusinessType == type).ToList();
                if (!ans.Any())
                {
                    return false;
                }
                return true;
            
        }

    }
}
