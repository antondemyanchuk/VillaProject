using VillaProject_WEB.Models;

namespace VillaProject_WEB.Services.IServices
{
    public interface IBaseService
    {
        APIResponse ResponseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
