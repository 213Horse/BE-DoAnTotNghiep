namespace CleanMinimalApi.Application.Authentications;
using System.IdentityModel.Tokens.Jwt;

public interface IAuthenticationRepository
{
    public Task<bool> Register(string email, string username, string fullName, string password, CancellationToken cancellationToken);
    public Task<bool> EmailConfirm(string userId, string token, CancellationToken cancellationToken);
    public Task<JwtSecurityToken> Login(string username, string password, CancellationToken cancellationToken);
}
