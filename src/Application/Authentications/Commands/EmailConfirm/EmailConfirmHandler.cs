namespace CleanMinimalApi.Application.Authentications.Commands.EmailConfirm;
using CleanMinimalApi.Application.Authentications.Commands.EmailConfirm;
using MediatR;

public class EmailConfirmHandler(IAuthenticationRepository authenticationRepository) : IRequestHandler<EmailConfirmCommand, bool>
{
    public async Task<bool> Handle(EmailConfirmCommand request, CancellationToken cancellationToken)
    {
        return await authenticationRepository
            .EmailConfirm(request.UserId, request.Token, cancellationToken);
    }
}
