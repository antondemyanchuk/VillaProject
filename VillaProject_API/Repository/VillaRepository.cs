using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaProject_API.Data;
using VillaProject_API.Models;
using VillaProject_API.Repository;
using VillaProject_API.Repository.IRepository;

namespace VillaProject_API.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        private readonly AppDbContext _appDbContext;

        public VillaRepository(AppDbContext appDbContext):base(appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdateDate = DateTime.Now;
            _appDbContext.Update(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
