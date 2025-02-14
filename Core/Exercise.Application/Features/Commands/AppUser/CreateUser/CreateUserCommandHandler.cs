﻿using Exercise.Application.Exceptions;
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
		readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

		public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			IdentityResult result = await _userManager.CreateAsync(new()
			{
				Id=Guid.NewGuid().ToString(),
				UserName = request.UserName,
				Email = request.Email,
				NameSurname = request.NameSurname,
			}, request.Password);

			CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };
			if (result.Succeeded)
			{
				response.Message = "Kullanıcı başarıyla oluşturuldu.";
			}
			else
			{
				foreach (var error in result.Errors)
				{
					response.Message += $"{error.Code} - {error.Description}<br>";
				}
			}
			return response;
		}
	}
}
