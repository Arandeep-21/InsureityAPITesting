using InsureityAPI.Models;

namespace InsureityAPI.Repository
{
    public class BusinessMastersRepo : GeneralRepo<BusinessMaster>, IBusinessMastersRepo
    {
        private readonly InsureityContext _db;

        public BusinessMastersRepo(InsureityContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(BusinessMaster entity)
        {
            _db.BusinessMaster.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
