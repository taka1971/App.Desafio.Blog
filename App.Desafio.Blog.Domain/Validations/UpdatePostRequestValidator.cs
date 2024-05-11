using App.Desafio.Blog.Domain.Dtos.Requests;
using FluentValidation;

namespace App.Desafio.Blog.Domain.Validations
{
    public class UpdatePostRequestValidator : AbstractValidator<UpdatePostRequest>
    {
        public UpdatePostRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required")
                .MaximumLength(150).WithMessage("Cannot exceed the maximum length of 150 characters");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
        }
    }
}
