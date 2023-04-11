using InsureityAPI.Models;

namespace InsureityAPI.Repository
{
    public interface IPolicyMasterRepo<T>
    {
        public void AddPolicyMaster(T b);
        public List<T> GetAllPolicyMaster();
        public void UpdatePolicyMaster(int id, T b);
        public T ViewPolicyMaster(int id);
        public void DeletePolicyMaster(int id);

    }
}
