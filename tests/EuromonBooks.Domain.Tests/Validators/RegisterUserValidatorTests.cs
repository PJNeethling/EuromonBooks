using AutoFixture;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Exceptions;
using EuromonBooks.Domain.Validators;
using EuromonBooks.TestHelpers;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Mobecom.Mosaic.Domain.Tests.Validator
{
    public class RegisterUserValidatorTests
    {
        public readonly RegisterUserValidator _validator;

        public RegisterUserValidatorTests()
        {
            _validator = new RegisterUserValidator();
        }

        [FactAutoDisplayName]
        public async Task RegisterUserValidator_FailsForMissingRequiredFields()
        {
            var request = new UserModel();

            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage(ValidationMessages.IsRequired);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage(ValidationMessages.IsRequired);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task RegisterUserValidator_FailsForIncorrectEmailFormat(
            IFixture fixture)
        {
            var request = fixture.Build<UserModel>()
                .With(x => x.Email, "incorrectmail")
                .Create();

            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage(ValidationMessages.IsInvalid);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task ValidatorFormatsMessagesCorrectly_For_RegisterUserValidator(
            IFixture fixture)
        {
            var request = fixture.Build<UserModel>()
               .Without(x => x.Email)
               .Without(x => x.Password)
               .Create();

            var mosaicValidator = new EuromonBooksValidator(new List<FluentValidation.IValidator>
            {
                _validator
            });

            var exc = await Assert.ThrowsAsync<ValidationException>(() => mosaicValidator.ValidateAsync<RegisterUserValidator>(request));

            Assert.Equal(2, exc.Errors.Count);
            Assert.Contains(exc.Errors, m => m.Message.Equals("Email is required"));
            Assert.Contains(exc.Errors, m => m.Message.Equals("Password is required"));
            Assert.Equal("Bad Request", exc.Message);

            request = fixture.Build<UserModel>()
               .With(x => x.Email, "myemail")
               .Create();

            exc = await Assert.ThrowsAsync<ValidationException>(() => mosaicValidator.ValidateAsync<RegisterUserValidator>(request));

            Assert.Single(exc.Errors);
            Assert.Contains(exc.Errors, m => m.Message.Equals("Email is invalid"));
            Assert.Equal("Bad Request", exc.Message);
        }
    }
}