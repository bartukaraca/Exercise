﻿using Exercise.Application.Features.Commands.AppUser.LoginUser;
using Exercise.Application.Features.Commands.AppUser.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[Action]")]
		public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
		{
			LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
			return Ok(response);
		}
		[HttpPost("[action]")]
		public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
		{
			RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
			return Ok(response);
		}
	}
}
