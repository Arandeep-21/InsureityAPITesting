namespace InsureityAPI.Repository
{
    public interface IUnitOfWork
    {
        IBusinessMastersRepo BusinessMasters { get; }

        IPropertyMastersRepo PropertyMasters { get; }
        void Dispose();
        void Save();
    }
}
