using InsureityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsureityAPI.Repository
{
    public class PropertyRepo : IPropertyRepo<Property>
    {
        private readonly InsureityContext db;
        public PropertyRepo(InsureityContext _db)
        {
            db = _db;
        }
        public void AddProperty(Property property) //REST End Point POST: /CreateBusinessProperty
        {
            db.Properties.Add(property);
            db.SaveChanges();
        }

        public void UpdateProperty(int PropertyId, Property property) //REST End Point PUT: /UpdateBusinessProperty
        {
            db.Entry(property).State=EntityState.Modified;
            db.SaveChanges();
        }

        public Property ViewProperty(int PropertyId) //REST End Point GET: /ViewBusinessProperty
        {
            var property = db.Properties.Include(x=>x.Business).Include(x=>x.PropertyMaster).SingleOrDefault(x=>x.PropertyId==PropertyId);
            return property;
        }

        public void DeleteProperty(int PropertyId) //REST End Point DELETE: /DeleteBusinessProperty
        {
            var property = db.Properties.Find(PropertyId);
            db.Properties.Remove(property);
            db.SaveChanges();
        }

        public List<Property> GetPropertiesForBusiness(int BusinessId) //GET all properties per Business
        {
            return db.Properties.Include(x => x.Business).Include(x => x.PropertyMaster).Where(x => x.BusinessID == BusinessId).ToList();
        }

        public List<Property> GetAllProperties() //GET all Properties
        {
            return db.Properties.Include(x => x.Business).Include(x => x.PropertyMaster).ToList();
        }

        public void InsureProperty(int id)
        {
            Property p = db.Properties.Find(id);
            p.IsInsured = true;
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
