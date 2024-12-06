namespace CleanMinimalApi.Application.Authentications.Queries.Login;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

public class LoginHandler(IAuthenticationRepository repository) : IRequestHandler<LoginQuery, JwtSecurityToken>
{
    public async Task<JwtSecurityToken> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await repository.Login(request.Username, request.Password, cancellationToken);
    }
}

