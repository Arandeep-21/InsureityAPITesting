using InsureityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsureityAPI.Repository
{
    public class BusinessRepo: IBusinessRepo<Business>
    {
        private readonly InsureityContext _context;
        public BusinessRepo(InsureityContext context)
        {
            _context = context;
        }

        public void AddBusiness(Business b)
        {
            _context.Businesses.Add(b);
            _context.SaveChanges();
        }

        public void UpdateBusiness(int id, Business b)
        {
            _context.Businesses.Update(b);
            _context.SaveChanges();
        }

        public Business ViewBusiness(int id)
        {
            return _context.Businesses.Include(x => x.Consumer).Include(x => x.Consumer.Agent).Include(x => x.BusinessMaster).Include(x => x.Properties).FirstOrDefault(x => x.BusinessId == id);
        }
        public void DeleteBusiness(Business b)
        {
            _context.Remove(b);
            _context.SaveChanges();
        }
        public List<Business> GetBusinessForConsumer(int ConsumerId)
        {
            return _context.Businesses.Include(x => x.Consumer).Include(x => x.Consumer.Agent).Include(x => x.BusinessMaster).Where(x => x.ConsumerId == ConsumerId).ToList();
        }

        public List<Business> GetAllBusiness()
        {
            return _context.Businesses.Include(x => x.Consumer).Include(x => x.Consumer.Agent).Include(x => x.BusinessMaster).ToList();
        }
    }
}
