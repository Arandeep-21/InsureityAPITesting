using InsureityAPI.Models;

namespace InsureityAPI.Repository
{
    public class PropertyMastersRepo : GeneralRepo<PropertyMaster>, IPropertyMastersRepo
    {
        private readonly InsureityContext _db;

        public PropertyMastersRepo(InsureityContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(PropertyMaster entity)
        {
            _db.PropertyMaster.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
