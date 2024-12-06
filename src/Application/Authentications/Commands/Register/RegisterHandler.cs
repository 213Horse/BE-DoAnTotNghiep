namespace CleanMinimalApi.Application.Authentications.Commands.Register;
using System.Threading.Tasks;
using MediatR;

public class RegisterHandler(IAuthenticationRepository authenticationRepository) : IRequestHandler<RegisterCommand, bool>
{
    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await authenticationRepository
            .Register(request.Email, request.Username, request.FullName, request.Password, cancellationToken);
    }
}
