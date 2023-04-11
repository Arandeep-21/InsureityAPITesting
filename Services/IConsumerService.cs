namespace InsureityAPI.Services
{
    public interface IConsumerService<T>
    {
        List<T> GetAllConsumers();
        T GetConsumerById(int id);
        List<T> GetConsumersForAgent(int agentId);
        void CreateConsumer(T consumer);
        void UpdateConsumer(int id, T consumer);
        void DeleteConsumer(T consumer);
    }
}
