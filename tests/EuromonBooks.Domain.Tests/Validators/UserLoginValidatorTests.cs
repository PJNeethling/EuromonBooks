using EuromonBooks.Abstractions.Models;
using EuromonBooks.Domain.Validators;
using EuromonBooks.TestHelpers;
using FluentValidation.TestHelper;
using System.Threading.Tasks;

namespace Mobecom.Mosaic.Domain.Tests.Validator
{
    public class UserLoginValidatorTests
    {
        public readonly UserLoginValidator _validator;

        public UserLoginValidatorTests()
        {
            _validator = new UserLoginValidator();
        }

        [FactAutoDisplayName]
        public async Task UserLoginValidator_FailsForMissingRequiredFields()
        {
            var request = new UserLoginRequest();

            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.UserNameOrEmail)
                .WithErrorMessage(ValidationMessages.IsRequired);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage(ValidationMessages.IsRequired);
        }
    }
}