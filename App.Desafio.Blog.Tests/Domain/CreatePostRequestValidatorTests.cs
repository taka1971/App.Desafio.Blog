using FluentValidation.TestHelper;
using App.Desafio.Blog.Domain.Validations;
using App.Desafio.Blog.Domain.Dtos.Requests;

namespace App.Desafio.Blog.Tests.Domain
{
    public class CreatePostRequestValidatorTests
    {
        private readonly CreatePostRequestValidator _validator;

        public CreatePostRequestValidatorTests()
        {
            _validator = new CreatePostRequestValidator();
        }

        [Fact]
        public void Should_have_error_when_Title_is_null()
        {
            var model = new CreatePostRequest(null, "Valid content");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Title)
                  .WithErrorMessage("Title is required");
        }

        [Fact]
        public void Should_have_error_when_Title_is_too_long()
        {
            var model = new CreatePostRequest(new string('a', 151), "Valid content");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Title)
                  .WithErrorMessage("Cannot exceed the maximum length of 100 characters");
        }

        [Fact]
        public void Should_have_error_when_Content_is_null()
        {
            var model = new CreatePostRequest("Valid title", null);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Content)
                  .WithErrorMessage("Content is required");
        }

        [Fact]
        public void Should_not_have_error_when_request_is_valid()
        {
            var model = new CreatePostRequest("Valid title", "Valid content");
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(request => request.Title);
            result.ShouldNotHaveValidationErrorFor(request => request.Content);
        }
    }
}
