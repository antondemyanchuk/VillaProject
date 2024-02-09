using Newtonsoft.Json;
using System.Text;
using System.Net;
using VillaProject_WEB.Models;
using VillaProject_WEB.Services.IServices;
using VillaProject_Utility;
using System.Net.Http.Headers;

namespace VillaProject_WEB.Services
{
	public class BaseService : IBaseService
	{
		public APIResponse ResponseModel { get; set; }
		public IHttpClientFactory HttpClient { get; set; }
		public BaseService(IHttpClientFactory httpClient)
		{
			this.ResponseModel = new();
			this.HttpClient = httpClient;
		}

		public async Task<T> SendAsync<T>(APIRequest apiRequest)
		{

			var client = HttpClient.CreateClient("MagicAPI");
			HttpRequestMessage message = new();
			message.Headers.Add("Accept", "application/json");
			message.RequestUri = new Uri(apiRequest.Url);
			if (apiRequest.Data != null)
			{
				message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
			}
			switch (apiRequest.ApiType)
			{
				case SD.ApiType.POST:
					message.Method = HttpMethod.Post;
					break;
				case SD.ApiType.PUT:
					message.Method = HttpMethod.Put;
					break;
				case SD.ApiType.DELETE:
					message.Method = HttpMethod.Delete;
					break;
				default:
					message.Method = HttpMethod.Get;
					break;
			}
			if (!string.IsNullOrEmpty(apiRequest.Token))
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
			}

			HttpResponseMessage apiResponse = null;
			apiResponse = await client.SendAsync(message);
			var apiContent = await apiResponse.Content.ReadAsStringAsync();
			try
			{
				APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
				if (apiResponse != null && (apiResponse.StatusCode == HttpStatusCode.BadRequest ||
					apiResponse.StatusCode == HttpStatusCode.NotFound))
				{
					ApiResponse.StatusCode = HttpStatusCode.BadRequest;
					ApiResponse.IsSuccess = false;
					var res = JsonConvert.SerializeObject(ApiResponse);
					var returnObj = JsonConvert.DeserializeObject<T>(res);
					return returnObj;
				}
			}
			catch (Exception ex)
			{
				var exeptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
				return exeptionResponse;
			}

			var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);

			return APIResponse;

		}
	}
}
