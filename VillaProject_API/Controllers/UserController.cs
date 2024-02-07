using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaProject_API.Models;
using VillaProject_API.Models.DTO;
using VillaProject_API.Repository.IRepository;

namespace VillaProject_API.Controllers
{
	[Route("api/UserAuth")]
	[ApiController]
	[ApiVersionNeutral]
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;
		protected APIResponse _response;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
			this._response = new APIResponse();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
		{
			var loginResponse = await _userRepository.Login(model);
			if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
			{
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.ErrorMessages = ["Username or password is incorrect"];
				return BadRequest(_response);
			}
			_response.IsSuccess = true;
			_response.StatusCode = HttpStatusCode.OK;
			_response.Result = loginResponse;
			return Ok(_response);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
		{
			bool isUserNameUnique = await _userRepository.IsUniqueUser(model.UserName);
			if (!isUserNameUnique)
			{
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.ErrorMessages = ["UserName already exist"];
				return BadRequest(_response);
			}
			var user = await _userRepository.Register(model);
			if (user == null) 
			{
				_response.IsSuccess = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.ErrorMessages = ["Error while registering"];
				return BadRequest(_response);
			}
			_response.IsSuccess = true;
			_response.StatusCode = HttpStatusCode.OK;

			return Ok(_response);
		}
	}
}
