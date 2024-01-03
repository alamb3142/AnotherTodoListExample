using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Common;

public interface IJwtTokenService
{
	public string GenerateToken(int userId);
}

public class JwtTokenService : IJwtTokenService
{
	public string GenerateToken(int userId)
	{
		var securityKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes("AStupidStupidStupidStuipdStuidasldkfjsalkfjsadfSecret")
		);
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new[] { new Claim("user_id", userId.ToString()) };

		var token = new JwtSecurityToken(
			issuer: null,
			audience: null,
			claims: claims,
			expires: DateTime.UtcNow.AddHours(1),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
