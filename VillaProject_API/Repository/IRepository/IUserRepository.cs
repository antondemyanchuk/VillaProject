using VillaProject_API.Models;
using VillaProject_API.Models.DTO;

namespace VillaProject_API.Repository.IRepository
{
	public interface IUserRepository
	{
		Task<bool> IsUniqueUser(string username);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);
		Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
	}
}
