using InsureityAPI.Models;
using InsureityAPI.Repository;

namespace InsureityAPI.Services
{
    public class ConsumerService : IConsumerService<Consumer>
    {
        private readonly IConsumerRepo<Consumer> _consumerRepo;
        public ConsumerService(IConsumerRepo<Consumer> consumerRepo)
        {
            _consumerRepo = consumerRepo;
        }
        public void CreateConsumer(Consumer consumer)
        {
            try
            {
                _consumerRepo.CreateConsumer(consumer);
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
                _consumerRepo.DeleteConsumer(consumer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Consumer> GetAllConsumers()
        {
            try
            {
                return _consumerRepo.GetAllConsumers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Consumer GetConsumerById(int id)
        {
            try
            {
                return _consumerRepo.GetConsumerById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Consumer> GetConsumersForAgent(int agentId)
        {
            try
            {
                return _consumerRepo.GetConsumersForAgent(agentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateConsumer(int id, Consumer consumer)
        {
            try
            {
                _consumerRepo.UpdateConsumer(id, consumer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}