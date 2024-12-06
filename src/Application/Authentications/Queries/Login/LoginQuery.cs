namespace CleanMinimalApi.Application.Authentications.Queries.Login;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using MediatR;

public class LoginQuery : IRequest<JwtSecurityToken>
{
    [Required]
    public string Username { get; init; }
     
    [Required]
    public string Password { get; init; }


}
