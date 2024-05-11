using FluentValidation.TestHelper;
using App.Desafio.Blog.Domain.Validations;
using App.Desafio.Blog.Domain.Dtos.Requests;

namespace App.Desafio.Blog.Tests.Domain
{
    public class UserLoginRequestValidatorTests
    {
        private readonly UserLoginRequestValidator _validator;

        public UserLoginRequestValidatorTests()
        {
            _validator = new UserLoginRequestValidator();
        }

        [Fact]
        public void Should_have_error_when_Email_is_null()
        {
            var model = new UserLoginRequest(null, "password123");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Email);            
        }

        [Fact]
        public void Should_have_error_when_Email_is_not_a_valid_email()
        {
            var model = new UserLoginRequest("invalid-email", "password123");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Email)
                  .WithErrorMessage("A valid email is required");
        }

        [Fact]
        public void Should_have_error_when_Password_is_too_short()
        {
            var model = new UserLoginRequest("email@challenger.com", "pass");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Password)
                  .WithErrorMessage("Password must be at least 6 characters");
        }

        [Fact]
        public void Should_not_have_error_when_request_is_valid()
        {
            var model = new UserLoginRequest("email@challenger.com", "password123");
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(request => request.Email);
            result.ShouldNotHaveValidationErrorFor(request => request.Password);
        }
    }
}
