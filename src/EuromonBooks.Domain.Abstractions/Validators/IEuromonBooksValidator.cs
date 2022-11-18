namespace EuromonBooks.Domain.Abstractions.Validators
{
    public interface IEuromonBooksValidator
    {
        Task ValidateAsync<T>(object data) where T : class;
    }
}