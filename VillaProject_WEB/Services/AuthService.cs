using VillaProject_WEB.Models;
using VillaProject_WEB.Models.DTO;
using VillaProject_WEB.Services.IServices;
using static VillaProject_Utility.SD;

namespace VillaProject_WEB.Services
{
	public class AuthService : BaseService, IAuthService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private string _url;

		public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration):base(httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
			_url = configuration.GetValue<string>("ServiceUrls:VillaAPI");
		}

		public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.POST,
				Data = loginRequestDTO,
				Url = _url + "/api/UserAuth/login"
			});
		}

		public Task<T> RegisterAsync<T>(RegistrationRequestDTO registrationRequestDTO)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = ApiType.POST,
				Data = registrationRequestDTO,
				Url = _url + "/api/UserAuth/register"
			});
		}
	}
}
