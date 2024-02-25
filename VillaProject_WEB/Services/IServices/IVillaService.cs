using VillaProject_WEB.Models.DTO;

namespace VillaProject_WEB.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>(string token, string? search = null, int? occupancy = null, int? recordsPerPage = null, int? pageNumber = null);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
