using VillaProject_WEB.Models;
using VillaProject_WEB.Models.DTO;
using VillaProject_WEB.Services.IServices;
using static VillaProject_Utility.SD;

namespace VillaProject_WEB.Services
{
	public class VillaNumberService : BaseService, IVillaNumberService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _url;
		public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_url = configuration.GetValue<string>("ServiceUrls:VillaAPI");
		}

		public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.POST,
				Data = dto,
				Url = _url + "/api/v1/VillaNumber",
				Token = token
			});
		}

		public Task<T> DeleteAsync<T>(int VillaNo, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.DELETE,
				Url = _url + "/api/v1/VillaNumber/" + VillaNo,
				Token = token
			});
		}

		public Task<T> GetAllAsync<T>(string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.GET,
				Url = $"{_url}/api/v1/VillaNumber",
				Token = token
			});
		}

		public Task<T> GetAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.GET,
				Url = $"{_url}/api/v1/VillaNumber/{id}",
				Token = token
			});
		}

		public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.PUT,
				Data = dto,
				Url = $"{_url}/api/v1/VillaNumber/{dto.VillaNo}",
				Token = token
			});
		}
	}
}
