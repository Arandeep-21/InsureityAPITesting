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
    public class QuotesController : ControllerBase
    {
        private readonly InsureityContext _context;
        private readonly IPropertyService<Property> _propService;
        private readonly ILogger<QuotesController> logger;

        public QuotesController(InsureityContext context, IPropertyService<Property> propertyService, ILogger<QuotesController> logger)
        {
            _context = context;
            _propService = propertyService;
            this.logger = logger;
        }

        // GET: api/Quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            logger.LogInformation($"All quoted viewed");
          if (_context.Quotes == null)
          {
              return NotFound();
          }
            return await _context.Quotes.ToListAsync();
        }

        [HttpGet("business")]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotesByBusiness(int bid)
        {
            if (_context.Quotes == null)
            {
                return NotFound();
            }
            return await _context.Quotes.Include(x=>x.Policies).Where(x=>x.BusinessID == bid).ToListAsync();
        }

        // GET: api/Quotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            logger.LogInformation($"Quote {id} viewed");
          if (_context.Quotes == null)
          {
              return NotFound();
          }
            var quote = await _context.Quotes.Include(x=>x.Policies).Where(x=>x.QuoteId == id).SingleOrDefaultAsync();

            if (quote == null)
            {
                return NotFound();
            }
            quote.Business = await _context.Businesses.FindAsync(quote.BusinessID);
            quote.Consumer = await _context.Consumers.FindAsync(quote.ConsumerId);

            return quote;
        }

        // PUT: api/Quotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuote(int id, Quote quote)
        {
            logger.LogInformation($"Quote {id} Updated");
            if (id != quote.QuoteId)
            {
                return BadRequest();
            }

            _context.Entry(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(id))
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

        // POST: api/Quotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote(Quote quote)
        {
            logger.LogInformation($"Quote {quote.QuoteId} added");
          if (_context.Quotes == null)
          {
              return Problem("Entity set 'InsureityContext.Quotes'  is null.");
          }
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuote", new { id = quote.QuoteId }, quote);
        }

        // DELETE: api/Quotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            logger.LogInformation($"Quote {id} Deleted");
            if (_context.Quotes == null)
            {
                return NotFound();
            }
            var quote = await _context.Quotes.Include(x=>x.Policies).Where(x=>x.QuoteId == id).SingleOrDefaultAsync();
            if (quote == null)
            {
                return NotFound();
            }
            foreach(Policy p in quote.Policies)
            {
                p.QuoteId = null;
                _context.Entry(p).State = EntityState.Modified;

            }
            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("createQuote")]
        public async Task<IActionResult> CreateQuote(int bid)
        {
            logger.LogInformation($"Quote Created");
            Quote quote = new();
            Business b = _context.Businesses.Find(bid);
            IEnumerable<Property> properties = await _context.Properties.Where(x => x.BusinessID == bid && !x.IsInsured).ToListAsync();
            if(properties.Count() < 1)
            {
                return BadRequest("No eligible properties to insure");
            }
            quote.BusinessID = bid;
            quote.ConsumerId = b.ConsumerId;
            _context.Quotes.Add(quote);
            _context.SaveChanges();
            foreach (Property property in properties)
            {
                Policy policy = _context.Policies.Where(x => x.PropertyId == property.PropertyId).SingleOrDefault();
                if (policy == null || policy.QuoteId != null)
                    continue;
                policy.QuoteId = quote.QuoteId;
                quote.QuoteAmount += policy.PremiumAmount;
            }
            if (quote.QuoteAmount > 0)
            {
                _context.Entry(quote).State = EntityState.Modified;
            }
            else
            {
                _context.Remove(quote);
                _context.SaveChanges();
                return BadRequest("All existing policies are already included in a quote! Create new policies or Delete existing quote!");
            }
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(_context.Quotes.Include(x =>x.Policies).Where(x =>x.QuoteId == quote.QuoteId).SingleOrDefault());

        }
        [HttpGet("IssueQuote")]
        public async Task<IActionResult> IssueQuote(int id)
        {
            logger.LogInformation($"Quote {id} issued");
            Quote q = _context.Quotes.Include(x=>x.Policies).Where(x=>x.QuoteId == id).SingleOrDefault();
            if(q == null)
            {
                return NotFound();
            }
            foreach(Policy policy in q.Policies)
            {
                policy.PolicyStatus = "Issued";
                policy.IssuedAt = DateTime.Now;
                _context.Entry(policy).State = EntityState.Modified;
                _propService.InsureProperty(policy.PropertyId);
            }
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return Problem("An error occured!");
            }
            return Ok();
        }


        private bool QuoteExists(int id)
        {
            logger.LogInformation($"Quote {id} checked");
            return (_context.Quotes?.Any(e => e.QuoteId == id)).GetValueOrDefault();
        }
    }
}
