using Exercise.Application.Abstractions.Services;
using Exercise.Application.Abstractions.Token;
using Exercise.Application.DTOs;
using Exercise.Application.Exceptions;
using Exercise.Application.Features.Commands.AppUser.LoginUser;
using Exercise.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Services
{
	public class AuthService : IAuthService
	{
		readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
		readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
		readonly ITokenHandler _tokenHandler;
		readonly IUserService _userService;

		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenHandler = tokenHandler;
			_userService = userService;
		}

		public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
		{
			Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
			if (user == null)
				user = await _userManager.FindByEmailAsync(usernameOrEmail);

			if (user == null)
				throw new NotFoundUserException();

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
			if (result.Succeeded) //Authentication başarılı!
			{
				Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
				return token;
			}
			throw new AuthenticationErrorException();
		}
		public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
		{
			AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
			{
				Token token = _tokenHandler.CreateAccessToken(15);
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
				return token;
			}
			else
				throw new NotFoundUserException();
		}
	}
}