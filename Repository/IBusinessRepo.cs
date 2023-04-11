namespace InsureityAPI.Repository
{
    public interface IBusinessRepo<B>
    {
        void AddBusiness(B b);
        void UpdateBusiness(int id, B b);
        B ViewBusiness(int id);
        List<B> GetAllBusiness();
        void DeleteBusiness(B b);
        List<B> GetBusinessForConsumer(int ConsumerId);


    }
}
