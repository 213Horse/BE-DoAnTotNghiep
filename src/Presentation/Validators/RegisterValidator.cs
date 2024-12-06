namespace CleanMinimalApi.Presentation.Validators;

using CleanMinimalApi.Presentation.Requests;
using FluentValidation;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        _ = this.RuleFor(r => r.Email).NotEqual(string.Empty).WithMessage("Please provide email");
        _ = this.RuleFor(r => r.FullName).NotEqual(string.Empty).WithMessage("Please provide name");
        _ = this.RuleFor(r => r.Username).NotEqual(string.Empty).WithMessage("Please provide username");
        _ = this.RuleFor(r => r.Password).NotEmpty().WithMessage("Please provide password");
    }
}
