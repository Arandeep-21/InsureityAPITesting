using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace InsureityAPI.Repository
{
    public interface IGeneralRepo<T> where T : class
    {
        Task<ActionResult<List<T>>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
