namespace CleanMinimalApi.Application.Authentications.Commands.Register;
using MediatR;

public class RegisterCommand : IRequest<bool>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string FullName { get; init; }
    public string Password { get; init; }
}
