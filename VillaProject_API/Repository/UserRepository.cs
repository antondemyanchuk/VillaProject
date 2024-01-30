using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VillaProject_API.Data;
using VillaProject_API.Models;
using VillaProject_API.Models.DTO;
using VillaProject_API.Repository.IRepository;

namespace VillaProject_API.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;
		private string _secretKey;
		public UserRepository(AppDbContext context, IConfiguration configuration) 
		{
			_context = context;
			_secretKey = configuration.GetValue<string>("ApiSettings:Secret");
		}

		public bool IsUniqueUser(string username)
		{
			var user = _context.LocalUsers.FirstOrDefault(u => u.UserName == username);
			if (user == null)
			{
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
		{
			var user = _context.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.UserName.ToLower() && u.Password == loginRequest.Password);
			if (user == null)
			{
				return null;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_secretKey);

			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new(ClaimTypes.Name, user.Id.ToString()),
					new(ClaimTypes.Role, user.Role)
				}),
				Expires = DateTime.UtcNow.AddMinutes(30),
				SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			LoginResponseDTO loginResponseDTO = new() 
			{
				Token = tokenHandler.WriteToken(token),
				User = user
			};

			return loginResponseDTO;
		}

		public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
		{
			LocalUser user = new LocalUser()
			{
				UserName = registrationRequestDTO.UserName,
				Name = registrationRequestDTO.Name,
				Password = registrationRequestDTO.Password,
				Role = registrationRequestDTO.Role
			};

			await _context.LocalUsers.AddAsync(user);
			await _context.SaveChangesAsync();
			return user;
		}
	}
}
