namespace InsureityAPI.Repository
{
    public interface IConsumerRepo<T>
    {
        List<T> GetAllConsumers();
        T GetConsumerById(int id);
        List<T> GetConsumersForAgent(int agentId);
        void CreateConsumer(T consumer);
        void UpdateConsumer(int id, T consumer);
        void DeleteConsumer(T consumer);
    }
}
