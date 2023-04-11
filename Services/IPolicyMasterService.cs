namespace InsureityAPI.Services
{
    public interface IPolicyMasterService<T>
    {
        public void AddPolicyMaster(T pl);
        public void DeletePolicyMaster(int id);
        public List<T> GetAllPolicyMaster();
        public void UpdatePolicyMaster(int id, T pm);
        public T ViewPolicyMaster(int id);
    }
}
