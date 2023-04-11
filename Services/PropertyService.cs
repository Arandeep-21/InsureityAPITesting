using InsureityAPI.Models;
using InsureityAPI.Repository;

namespace InsureityAPI.Services
{
    public class PropertyService : IPropertyService<Property>
    {
        private readonly IPropertyRepo<Property> repoobj;

        public PropertyService(IPropertyRepo<Property> robj) 
        {
            repoobj = robj;
        }
        public void AddProperty(Property property)
        {
            property.DepreciationExpense = CalcDepreciationExpense(property.AssetCost, property.SalvageValue, property.UsefulLife);
            property.PropertyScore = CalcPropertyValue(property.DepreciationExpense, property.AssetCost);
            repoobj.AddProperty(property);
        }

        public void DeleteProperty(int PropertyId)
        {
            repoobj.DeleteProperty(PropertyId);
        }

        public void UpdateProperty(int PropertyId, Property property)
        {
            property.DepreciationExpense = CalcDepreciationExpense(property.AssetCost, property.SalvageValue, property.UsefulLife);
            property.PropertyScore = CalcPropertyValue(property.DepreciationExpense, property.AssetCost);
            repoobj.UpdateProperty(PropertyId, property);
        }

        public Property ViewProperty(int PropertyId)
        {
            return repoobj.ViewProperty(PropertyId);
        }

        public List<Property> GetPropertiesForBusiness(int BusinessId)
        {
            return repoobj.GetPropertiesForBusiness(BusinessId);
        }
        public List<Property> GetAllProperties()
        {
            return repoobj.GetAllProperties();
        }

        public void InsureProperty(int id)
        {
            repoobj.InsureProperty(id);
        }

        public double CalcDepreciationExpense(double AssetCost, double SalvageValue, int UsefulLife) //Formula to calculate the Depreciation expense, which inturn shall calculate the Property Score
        {
            return ((AssetCost - SalvageValue) / UsefulLife);
        }

        public int CalcPropertyValue(double? DepreciationExpense, double AssetCost) //Method to assign indexed values of the property score using the depreciation expense
        {
            double? result = (DepreciationExpense / AssetCost * 100);

            if (result > 0 && result <= 3)
                return 10;
            else if (result > 3 && result <= 6)
                return 9;
            else if (result > 6 && result <= 9)
                return 8;
            else if (result > 9 && result <= 12)
                return 7;
            else if (result > 12 && result <= 15)
                return 6;
            else if (result > 15 && result <= 18)
                return 5;
            else if (result > 18 && result <= 21)
                return 4;
            else if(result > 21 && result <= 24)
                return 3;
            else if(result > 24 && result <= 27)
                return 2;
            else if(result > 27)
                return 1;
            else
                return 0;
        }
    }
}
