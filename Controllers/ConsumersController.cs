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
    public class ConsumersController : ControllerBase
    {
        private readonly IConsumerService<Consumer> _consumerService;
        private readonly ILogger<ConsumersController> logger;
        public ConsumersController(IConsumerService<Consumer> cs, ILogger<ConsumersController> logger)
        {
            _consumerService = cs;
            this.logger = logger;
        }

        // GET: api/Consumers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumer>>> GetConsumers()
        {
            logger.LogInformation($"Get all consumers");
            return Ok(_consumerService.GetAllConsumers());
        }

        // GET: api/Consumers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consumer>> GetConsumer(int id)
        {
            logger.LogInformation($"{id} viewed");
            var consumer = _consumerService.GetConsumerById(id);

            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer);
        }

        [HttpGet("AgentConsumers")]
        public async Task<ActionResult<IEnumerable<Consumer>>> GetConsumersForAgent(int agentId)
        {
            logger.LogInformation($"{agentId} consumers list");
            return Ok(_consumerService.GetConsumersForAgent(agentId));
        }

        // PUT: api/Consumers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumer(int id, Consumer consumer)
        {
            logger.LogInformation($"{id} updated");
            if (id != consumer.ConsumerId)
            {
                return BadRequest();
            }


            try
            {
                _consumerService.UpdateConsumer(id, consumer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumerExists(id))
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

        // POST: api/Consumers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consumer>> PostConsumer(Consumer consumer)
        {
            logger.LogInformation($"{consumer.ConsumerId} consumer added");
            try
            {
                _consumerService.CreateConsumer(consumer);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            

            return CreatedAtAction("GetConsumer", new { id = consumer.ConsumerId }, consumer);
        }

        // DELETE: api/Consumers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumer(int id)
        {
            logger.LogInformation($"{id} Deleted");
            
            var consumer = _consumerService.GetConsumerById(id);
            if (consumer == null)
            {
                return NotFound();
            }
            try
            {
                _consumerService.DeleteConsumer(consumer);
                return NoContent();
            }
            catch(Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        private bool ConsumerExists(int id)
        {
            logger.LogInformation($"{id} Status");
            return _consumerService.GetConsumerById(id) != null;
        }
    }
}
