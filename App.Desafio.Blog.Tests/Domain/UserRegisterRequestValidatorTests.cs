using FluentValidation.TestHelper;
using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Validations;

namespace App.Desafio.Blog.Tests.Domain
{
    public class UserRegisterRequestValidatorTests
    {
        private readonly UserRegisterRequestValidator _validator;

        public UserRegisterRequestValidatorTests()
        {
            _validator = new UserRegisterRequestValidator();
        }

        [Fact]
        public void Should_have_error_when_Username_is_null()
        {
            var model = new UserRegisterRequest(null, "email@example.com", "password123");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Username)
                  .WithErrorMessage("Username is required");
        }

        [Fact]
        public void Should_have_error_when_Username_is_too_long()
        {
            var model = new UserRegisterRequest(new string('a', 101), "email@example.com", "password123");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Username)
                  .WithErrorMessage("Cannot exceed the maximum length of 100 characters");
        }

        [Fact]
        public void Should_have_error_when_Email_is_null()
        {
            var model = new UserRegisterRequest("username", null, "password123");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Email)
                  .WithErrorMessage("A valid email is required");
        }

        [Fact]
        public void Should_have_error_when_Email_is_not_a_valid_email()
        {
            var model = new UserRegisterRequest("username", "invalid-email", "password123");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Email)
                  .WithErrorMessage("A valid email is required");
        }

        [Fact]
        public void Should_have_error_when_Password_is_too_short()
        {
            var model = new UserRegisterRequest("username", "email@example.com", "pass");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Password)
                  .WithErrorMessage("Password must be at least 6 characters");
        }

        [Fact]
        public void Should_not_have_error_when_request_is_valid()
        {
            var model = new UserRegisterRequest("username", "email@example.com", "password123");
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(request => request.Username);
            result.ShouldNotHaveValidationErrorFor(request => request.Email);
            result.ShouldNotHaveValidationErrorFor(request => request.Password);
        }
    }

}
