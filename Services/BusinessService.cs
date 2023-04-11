using InsureityAPI.Models;
using InsureityAPI.Repository;

namespace InsureityAPI.Services
{
    public class BusinessService : IBusinessService<Business>
    {
        private readonly IBusinessRepo<Business> businessrepoobj;
        public BusinessService(IBusinessRepo<Business> _businessrepoobj)
        {
            businessrepoobj = _businessrepoobj;
        }

        public void AddBusiness(Business b)
        {
            b.ROI = CalcRoi(b.BusinessTurnover, b.CapitalInvested);
            b.BusinessScore = CalcBusinessScore(b.ROI);
            businessrepoobj.AddBusiness(b);
           
        }

        public void UpdateBusiness(int id, Business b)
        {
            b.ROI = CalcRoi(b.BusinessTurnover, b.CapitalInvested);
            b.BusinessScore = CalcBusinessScore(b.ROI);
            businessrepoobj.UpdateBusiness(id,b);
        }

        public Business ViewBusiness(int id)
        {
            return businessrepoobj.ViewBusiness(id);
        }

        public List<Business> GetAllBusiness()
        {
            return businessrepoobj.GetAllBusiness();
        }
        public void DeleteBusiness(Business b)
        {
            businessrepoobj.DeleteBusiness(b);
        }

        public double CalcRoi(double BusinessTurnover, double BusinessCapital)
        {
            return (BusinessTurnover / BusinessCapital) * 100;
        }

        public int? CalcBusinessScore(double? ROI)
        {
            
            if (ROI >= 1 && ROI <= 3)
            {
                return 1;
            }
            else if (ROI >= 4 && ROI <= 6)
            {
                return 2;
            }
            else if (ROI >= 7 && ROI <= 9)
            {
                return 3;
            }
            else if (ROI >= 10 && ROI <= 12)
            {
                return 4;
            }
            else if (ROI >= 13 && ROI <= 15)
            {
                return 5;
            }
            else if (ROI >= 16 && ROI <= 18)
            {
                return 6;
            }
            else if (ROI >= 19 && ROI <= 21)
            {
                return 7;
            }
            else if (ROI >= 22 && ROI <= 24)
            {
                return 8;
            }
            else if (ROI >= 25 && ROI <= 27)
            {
                return 9;
            }
            else if (ROI >= 28 && ROI <= 30)
            {
                return 10;
            }
            else
            {
                return 0;
            }
        }

        public List<Business> GetBusinessForConsumer(int ConsumerID)
        {
                try
                {
                    return businessrepoobj.GetBusinessForConsumer(ConsumerID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
    }
}

