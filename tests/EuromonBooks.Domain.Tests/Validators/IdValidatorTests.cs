using EuromonBooks.Domain.Abstractions.Exceptions;
using EuromonBooks.Domain.Validators;
using EuromonBooks.TestHelpers;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Mobecom.Mosaic.Domain.Tests.Validator
{
    public class IdValidatorTests
    {
        public readonly IdValidator _validator;

        public IdValidatorTests()
        {
            _validator = new IdValidator();
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task IdValidator_Throws_On_InvalidFields()
        {
            var id = 0;
            var result = await _validator.TestValidateAsync(id);

            result.ShouldHaveValidationErrorFor("Id")
                .WithErrorMessage(ValidationMessages.IsInvalid);

            id = -1;

            result = await _validator.TestValidateAsync(id);

            result.ShouldHaveValidationErrorFor("Id")
                .WithErrorMessage(ValidationMessages.IsInvalid);

            id = int.MinValue;

            result = await _validator.TestValidateAsync(id);

            result.ShouldHaveValidationErrorFor("Id")
                .WithErrorMessage(ValidationMessages.IsInvalid);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task IdValidator_DoesNotThrow_WhenValid(
            int id)
        {
            var result = await _validator.TestValidateAsync(id);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [FactAutoDisplayName]
        public async Task ValidatorFormatsMessagesCorrectly_For_IdValidator()
        {
            var id = -1;

            var mosaicValidator = new EuromonBooksValidator(new List<FluentValidation.IValidator>
            {
                _validator
            });

            var exc = await Assert.ThrowsAsync<ValidationException>(() => mosaicValidator.ValidateAsync<IdValidator>(id));

            Assert.Single(exc.Errors);
            Assert.Contains(exc.Errors, m => m.Message.Equals("Id is invalid"));
        }
    }
}