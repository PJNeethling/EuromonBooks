using EuromonBooks.Domain.Abstractions.Exceptions;
using EuromonBooks.Domain.Validators;
using EuromonBooks.TestHelpers;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Mobecom.Mosaic.Domain.Tests.Validator
{
    public class UserUuidValidatorTests
    {
        public readonly UserUuidValidator _validator;

        public UserUuidValidatorTests()
        {
            _validator = new UserUuidValidator();
        }

        [FactAutoDisplayName]
        public async Task UserUuidValidator_FailsForMissingRequiredFields()
        {
            var uUuid = string.Empty;

            var result = await _validator.TestValidateAsync(uUuid);

            result.ShouldHaveValidationErrorFor("Uuid")
                .WithErrorMessage(ValidationMessages.IsRequired);
        }

        [FactAutoDisplayName]
        public async Task ValidatorFormatsMessagesCorrectly_For_UserUuidValidator()
        {
            var uUuid = string.Empty;
            var mosaicValidator = new EuromonBooksValidator(new List<FluentValidation.IValidator>
            {
                _validator
            });

            var exc = await Assert.ThrowsAsync<ValidationException>(() => mosaicValidator.ValidateAsync<UserUuidValidator>(uUuid));

            Assert.Single(exc.Errors);
            Assert.Contains(exc.Errors, m => m.Message.Equals("Uuid is required"));
            Assert.Equal("Bad Request", exc.Message);
        }
    }
}