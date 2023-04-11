using InsureityAPI.Models;
using InsureityAPI.Repository;

namespace InsureityAPI.Services
{
    public class PolicyMasterService : IPolicyMasterService<PolicyMaster>
    {
        private readonly IPolicyMasterRepo<PolicyMaster> PolicyMasterrepoobj;
        public PolicyMasterService(IPolicyMasterRepo<PolicyMaster> _PolicyMasterrepoobj)
        {
            PolicyMasterrepoobj = _PolicyMasterrepoobj;
        }
        public void AddPolicyMaster(PolicyMaster pl)
        {
            PolicyMasterrepoobj.AddPolicyMaster(pl);
        }

        public void DeletePolicyMaster(int id)
        {
            PolicyMasterrepoobj.DeletePolicyMaster(id);
        }

        public List<PolicyMaster> GetAllPolicyMaster()
        {
            return PolicyMasterrepoobj.GetAllPolicyMaster();
        }

        public void UpdatePolicyMaster(int id, PolicyMaster pm)
        {
            PolicyMasterrepoobj.UpdatePolicyMaster(id, pm);
        }

        public PolicyMaster ViewPolicyMaster(int id)
        {
            return PolicyMasterrepoobj.ViewPolicyMaster(id);
        }
    }
}
