using App.Desafio.Blog.Domain.Dtos.Requests;
using FluentValidation;

namespace App.Desafio.Blog.Domain.Validations
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
