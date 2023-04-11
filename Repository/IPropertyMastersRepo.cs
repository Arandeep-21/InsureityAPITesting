using InsureityAPI.Models;

namespace InsureityAPI.Repository
{
    public interface IPropertyMastersRepo : IGeneralRepo<PropertyMaster>
    {
        Task UpdateAsync(PropertyMaster entity);
    }
}
