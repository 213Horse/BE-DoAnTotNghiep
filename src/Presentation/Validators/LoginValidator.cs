namespace CleanMinimalApi.Presentation.Validators;

using CleanMinimalApi.Presentation.Requests;
using FluentValidation;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        _ = this.RuleFor(r => r.Username).NotEmpty().WithMessage("Please provide username");
        _ = this.RuleFor(r => r.Password).NotEmpty().WithMessage("Please provide password");
    }
}
