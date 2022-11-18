using EuromonBooks.Abstractions.Exceptions;
using EuromonBooks.Domain.Abstractions.Models.ExceptionErrors;
using EuromonBooks.Domain.Abstractions.Validators;
using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace EuromonBooks.Domain.Validators
{
    public class EuromonBooksValidator : IEuromonBooksValidator
    {
        private readonly IEnumerable<IValidator> _validators;

        public EuromonBooksValidator(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public async Task ValidateAsync<T>(object data) where T : class
        {
            IValidator validator = GetValidator<T>();

            var context = new ValidationContext<object>(data);

            var result = await validator.ValidateAsync(context);

            if (!result.IsValid)
            {
                List<ErrorDetails> errors = CreateErrorDetailsList(result);

                throw new Abstractions.Exceptions.ValidationException("Bad Request", errors);
            }
        }

        private static List<ErrorDetails> CreateErrorDetailsList(ValidationResult result)
        {
            List<ErrorDetails> errors = new();
            foreach (var failure in result.Errors)
            {
                string errorMessage;

                if (failure.FormattedMessagePlaceholderValues == null)
                {
                    errorMessage = failure.ErrorMessage.Trim();
                }
                else
                {
                    string colString = GetCollectionIndex(failure);

                    if (failure.FormattedMessagePlaceholderValues["PropertyName"] != null)
                    {
                        errorMessage = $"{failure.FormattedMessagePlaceholderValues["PropertyName"].ToString().Replace(" ", "")}{colString} {failure.ErrorMessage}";
                    }
                    else
                    {
                        errorMessage = $"{failure.PropertyName}{colString} {failure.ErrorMessage}";
                    }
                }

                errors.Add(new ErrorDetails
                {
                    Message = errorMessage
                });
            }

            return errors;
        }

        private static string GetCollectionIndex(ValidationFailure failure)
        {
            string colString = null;
            failure.FormattedMessagePlaceholderValues.TryGetValue("CollectionIndex", out object colIndex);
            if (colIndex != null)
            {
                colString = $"[{colIndex}]";
            }

            return colString;
        }

        private IValidator GetValidator<T>() where T : class
        {
            var validator = _validators?.SingleOrDefault(svc => svc.GetType().Name == typeof(T).Name);

            if (validator == null)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, "No corresponding validator found for request");
            }

            return validator;
        }
    }
}