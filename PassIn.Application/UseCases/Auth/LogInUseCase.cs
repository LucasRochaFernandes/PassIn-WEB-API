using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Application.Helpers;
using PassIn.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace PassIn.Application.UseCases.Auth;
public class LogInUseCase
{
    private readonly PassInDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public LogInUseCase(PassInDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public ResponseLogIn Execute(RequestCredentials request)
    {
        var user = _dbContext.Users.FirstOrDefault(usr => usr.Username.Equals(request.Username));
        if (user is null) {
            throw new UnauthorizedException("Invalid Credentials");
        }
        var passwordMatch = Cryptography.CheckHashStringMatch(request.Password, user.Password);
        if (passwordMatch is false)
        {
            throw new UnauthorizedException("Invalid Credentials");
        }

        var jwtSettings = _configuration.GetSection("JwtSettings");

        var claims = new Claim[]
           {
                new Claim(ClaimTypes.Name, request.Username)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpirationInMinutes"])),
            signingCredentials: creds
        );
        var access_token = new JwtSecurityTokenHandler().WriteToken(token);
        return new ResponseLogIn { access_token = access_token };
    }
}
