namespace CleanMinimalApi.Application.Authentications.Commands.EmailConfirm;
using MediatR;

public class EmailConfirmCommand : IRequest<bool>
{
    public string UserId { get; init; }
    public string Token { get; init; }
}
