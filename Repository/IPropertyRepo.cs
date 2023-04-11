using InsureityAPI.Models;

namespace InsureityAPI.Repository
{
    public interface IPropertyRepo<Property>
    {
        void AddProperty(Property property);
        void UpdateProperty(int PropertyId, Property property);
        Property ViewProperty(int PropertyId);
        void DeleteProperty(int PropertyId);
        void InsureProperty(int id);
        public List<Property> GetPropertiesForBusiness(int BusinessId);
        public List<Property> GetAllProperties();
    }
}
