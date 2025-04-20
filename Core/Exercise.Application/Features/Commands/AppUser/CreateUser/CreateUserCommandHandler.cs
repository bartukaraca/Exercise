using Exercise.Application.Abstractions.Services;
using Exercise.Application.DTOs.User;
using Exercise.Application.Exceptions;
using Exercise.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Exercise.Application.Features.Commands.AppUser.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{
		readonly IUserService _userService;

		public CreateUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
	CreateUserResponse response =	await 	_userService.CreateAsync(new()
			{
				Email = request.Email,
				NameSurname = request.NameSurname,
				Password = request.Password,
				PasswordConfirm = request.PasswordConfirm,
				UserName= request.UserName,
			});
			return new()
			{
				Message = response.Message,
				Succeeded = response.Succeeded,
			};
		}
	}
}
