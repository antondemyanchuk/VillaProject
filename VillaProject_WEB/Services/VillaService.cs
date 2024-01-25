using VillaProject_WEB.Models;
using VillaProject_WEB.Models.DTO;
using VillaProject_WEB.Services.IServices;
using static VillaProject_Utility.SD;

namespace VillaProject_WEB.Services
{
	public class VillaService : BaseService, IVillaService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _url;
		public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_url = configuration.GetValue<string>("ServiceUrls:VillaAPI");
		}

		public Task<T> CreateAsync<T>(VillaCreateDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.POST,
				Data = dto,
				Url = _url + "/api/VillaProject"
			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.DELETE,
				Url = _url + "/api/VillaProject/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.GET,
				Url = $"{_url}/api/VillaProject"
			});
		}

		public Task<T> GetAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.GET,
				Url = $"{_url}/api/VillaProject/{id}"
			});
		}

		public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.PUT,
				Data = dto,
				Url = $"{_url}/api/VillaProject/{dto.Id}"
			});
		}
	}
}
