using System.Diagnostics.CodeAnalysis;

namespace EuromonBooks.Repository.Password_Manager.Infrastructure
{
    [ExcludeFromCodeCoverage]
    internal static class PasswordHashFactory
    {
        public static IPasswordHash GetInstance(PasswordHashComplexity complexity)
        {
            switch (complexity)
            {
                case PasswordHashComplexity.StandardScrypt:
                    return new ScryptHash(PasswordHashOptions.Standard);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}