using Exercise.Application.Features.Commands.AppUser.CreateUser;
using Exercise.Application.Features.Commands.AppUser.LoginUser;
using Exercise.Application.Features.Queries.CustomerQueries;
using Exercise.Domain.Entities;
using Exercise.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class UsersController : ControllerBase
	{
		readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
		{
			CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
			return Ok(response);
		}
		[HttpGet]
		public async Task<IActionResult> GetCustomer([FromQuery]GetAllCustomerQueryRequest getAllCustomerQueryRequest)
		{
		GetAllCustomerQueryResponse response =await _mediator.Send(getAllCustomerQueryRequest);
			return Ok(response);
		}
		}
	}



