using VillaProject_WEB.Models.DTO;

namespace VillaProject_WEB.Services.IServices
{
	public interface IAuthService
	{
		Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
		Task<T> RegisterAsync<T>(RegistrationRequestDTO registrationRequestDTO);

	}
}
