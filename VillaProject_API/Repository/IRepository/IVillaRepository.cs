using System.Linq.Expressions;
using VillaProject_API.Models;

namespace VillaProject_API.Repository.IRepository
{
    public interface IVillaRepository:IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}
