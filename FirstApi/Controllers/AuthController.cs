using FirstApi.Data;
using FirstApi.Dtos.User;
using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	private readonly IAuthRepository authRepo;

	public AuthController(IAuthRepository authRepo)
	{
		this.authRepo = authRepo;
	}

	[HttpPost]
	[Route("Register")]
	public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
	{
		var response = await this.authRepo.Register(
			new User { Username = request.Username }, request.Password
			);
		if(!response.Success)
		{
			return BadRequest(response);
		}

		return Ok(response);
	}

	[HttpPost]
	[Route("Login")]
	public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
	{
		var response = await this.authRepo.Login(request.Username, request.Password);
		if (!response.Success)
		{
			return BadRequest(response);
		}
		return Ok(response);
	}

	}
