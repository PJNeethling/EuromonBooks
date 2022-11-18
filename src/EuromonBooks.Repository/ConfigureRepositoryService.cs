using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Repository.Password_Manager;
using EuromonBooks.Repository.Password_Manager.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EuromonBooks.Repository
{
    public static class ConfigureRepositoryServices
    {
        public static IServiceCollection ConfigureRepository(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure(ConfigureEncryptionOptions(configuration))
                .AddScoped<ILoginRepository, LoginRepository>()
                .AddScoped<IPassword, Password>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IBookRepository, BookRepository>();
        }

        private static Action<EncryptionOptions> ConfigureEncryptionOptions(IConfiguration configuration)
        {
            return options =>
            {
                var section = configuration.GetSection("EncryptionOptions");
                section.Bind(options);
            };
        }
    }
}