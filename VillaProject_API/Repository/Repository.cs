using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaProject_API.Data;
using VillaProject_API.Models;
using VillaProject_API.Repository.IRepository;

namespace VillaProject_API.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDbContext _appDbContext;
		internal DbSet<T> _dbSet;

		public Repository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
			//_appDbContext.VillaNumbers.Include(v => v.Villa).ToList();
			this._dbSet = _appDbContext.Set<T>();
		}

		//"Villa, VillaSpecial"
		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,string? includeProperties = null, 
			int recordsPerPage = 3, int pageNumber = 1)
		{
			IQueryable<T> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (recordsPerPage > 0)
			{
				if (recordsPerPage > 100)
				{
					recordsPerPage = 100;
				}
				query = query.Skip(recordsPerPage * (pageNumber - 1)).Take(recordsPerPage);
			}

			if (includeProperties != null)
			{
				var properties = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);
				foreach (var property in properties)
				{
					query = query.Include(property);
				}
			}
			return await query.ToListAsync();
		}

		//"Villa, VillaSpecial"
		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
		{
			IQueryable<T> query = _dbSet;
			if (!tracked)
			{
				query = query.AsNoTracking();
			}

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if(includeProperties != null)
			{
				var properties = includeProperties.Split(',',StringSplitOptions.RemoveEmptyEntries); 
				foreach (var property in properties)
				{
					query = query.Include(property);
				}
			}

			return await query.FirstOrDefaultAsync();
		}


		public async Task CreateAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await SaveAsync();
		}

		public async Task RemoveAsync(T entity)
		{
			_dbSet.Remove(entity);
			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await _appDbContext.SaveChangesAsync();
		}

	}
}
