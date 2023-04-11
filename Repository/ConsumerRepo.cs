using InsureityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsureityAPI.Repository
{
    public class ConsumerRepo : IConsumerRepo<Consumer>
    {
        private readonly InsureityContext _db;
        public ConsumerRepo(InsureityContext db)
        {
            _db = db;
        }
        public void CreateConsumer(Consumer consumer)
        {
            try
            {
                _db.Consumers.Add(consumer);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteConsumer(Consumer consumer)
        {
            try
            {
                _db.Consumers.Remove(consumer);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Consumer> GetAllConsumers()
        {
            return _db.Consumers.ToList();
        }

        public Consumer GetConsumerById(int id)
        {
            return _db.Consumers.Include(x=>x.Agent).SingleOrDefault(x=>x.ConsumerId==id);
        }

        public List<Consumer> GetConsumersForAgent(int agentId)
        {
            return _db.Consumers.Where(x=>x.UserId == agentId).ToList();
        }

        public void UpdateConsumer(int id, Consumer consumer)
        {
            try
            {
                _db.Entry(consumer).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
