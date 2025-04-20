using Exercise.Application.DTOs.User;
using Exercise.Application.Features.Commands.AppUser.CreateUser;
using Exercise.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Abstractions.Services
{
	public interface IUserService
	{
		Task<CreateUserResponse> CreateAsync(CreateUser model);
		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenLifeTime);
	}
}
