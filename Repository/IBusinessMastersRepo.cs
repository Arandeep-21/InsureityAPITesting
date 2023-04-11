using InsureityAPI.Models;

namespace InsureityAPI.Repository
{
    public interface IBusinessMastersRepo : IGeneralRepo<BusinessMaster>
    {
        Task UpdateAsync(BusinessMaster entity);
    }
}
