namespace InsureityAPI.Services
{
    public interface IBusinessService<B>
    {
        void AddBusiness(B b);
        void UpdateBusiness(int id, B b);
        B ViewBusiness(int id);
        List<B> GetAllBusiness();
        void DeleteBusiness(B b);
        List<B> GetBusinessForConsumer(int ConsumerID);
        public double CalcRoi(double BusinessTurnover, double BusinessCapital);
        public int? CalcBusinessScore(double? ROI);

    }
}
