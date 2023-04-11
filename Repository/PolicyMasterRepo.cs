using InsureityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsureityAPI.Repository
{
    public class PolicyMasterRepo : IPolicyMasterRepo<PolicyMaster>
    {
        private readonly InsureityContext _context;
        public PolicyMasterRepo(InsureityContext context)
        {
            _context = context;
        }
        public void AddPolicyMaster(PolicyMaster b)
        {
            _context.PoliciesMaster.Add(b);
            _context.SaveChanges();
        }
        public List<PolicyMaster> GetAllPolicyMaster()
        {
            return _context.PoliciesMaster.ToList();
        }

        public void UpdatePolicyMaster(int id, PolicyMaster b)
        {
            _context.PoliciesMaster.Update(b);
            _context.SaveChanges();
        }
        public PolicyMaster ViewPolicyMaster(int id)
        {
            return _context.PoliciesMaster.Where(x => x.PlId == id).SingleOrDefault();
        }
        public void DeletePolicyMaster(int id)
        {
            PolicyMaster pm = _context.PoliciesMaster.Find(id);
            if(pm != null)
            {
                _context.Remove(pm);
                _context.SaveChanges();
            }
            
        }
    }
}
