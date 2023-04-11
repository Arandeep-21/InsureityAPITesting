namespace InsureityAPI.Services
{
    public interface IPropertyService<Property>
    {
        void AddProperty(Property property);
        void UpdateProperty(int PropertyId, Property property);
        Property ViewProperty(int PropertyId);
        void DeleteProperty(int PropertyId);
        void InsureProperty(int id);
        public double CalcDepreciationExpense(double AssetCost, double SalvageValue, int UsefulLife);
        public int CalcPropertyValue(double? DepreciationExpense, double AssetCost);
        public List<Property> GetPropertiesForBusiness(int BusinessId);
        public List<Property> GetAllProperties();
    }
}
