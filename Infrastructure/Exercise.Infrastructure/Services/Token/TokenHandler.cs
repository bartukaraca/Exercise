﻿using Exercise.Application.Abstractions.Token;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Infrastructure.Services.Token
{
	public class TokenHandler : ITokenHandler
	{
		readonly IConfiguration _configuration;

		public TokenHandler(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Application.DTOs.Token CreateAccessToken(int second)
		{
			Application.DTOs.Token token = new();

			//SecurityKey'in simetriğini alıyoruz.
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

			//Şifrelenmiş kimliği oluşturuyoruz.
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			//oluşturalacak token ayarlarını veriyoruz.
			token.Expiration = DateTime.UtcNow.AddSeconds(second);
			JwtSecurityToken securityToken = new(
				audience: _configuration["Token:Audience"],
				issuer: _configuration["Token:Issuer"],
				expires: token.Expiration,
				notBefore: DateTime.UtcNow,
				signingCredentials: signingCredentials
				);
			//Token oluşturucu sınıfından bir örnek alalım.
			JwtSecurityTokenHandler tokenHandler = new();
			token.AccessToken = tokenHandler.WriteToken(securityToken);

			token.RefreshToken = CreateRefreshToken();

			return token;
		}

		public string CreateRefreshToken()
		{
			byte[] number = new byte[32];
			using RandomNumberGenerator random = RandomNumberGenerator.Create();
			random.GetBytes(number);
			return Convert.ToBase64String(number);
		}
	}
}
