using InsureityAPI.Models;

namespace InsureityAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InsureityContext _db;
        public UnitOfWork(InsureityContext db)
        {
            _db = db;
            BusinessMasters = new BusinessMastersRepo(_db);
            PropertyMasters = new PropertyMastersRepo(_db);
        }
        public IBusinessMastersRepo BusinessMasters { get; private set; }
        public IPropertyMastersRepo PropertyMasters { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
