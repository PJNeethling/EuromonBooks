using EuromonBooks.Abstractions.Services;
using EuromonBooks.Abstractions.Services.JwtService;
using EuromonBooks.Domain.Abstractions.Services;
using EuromonBooks.Domain.Abstractions.Validators;
using EuromonBooks.Domain.Jwt;
using EuromonBooks.Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EuromonBooks.Domain
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure(ConfigureJwtAuthenticationServicesAndOptions(services, configuration));

            RegisterValidators(services);

            return services.AddScoped<IJwtService, JwtService>()
                .AddScoped<ILoginService, LoginService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IBookService, BookService>();
        }

        private static Action<JwtOptions> ConfigureJwtAuthenticationServicesAndOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("JWT");
            return options => section.Bind(options);
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            var assembly = typeof(IdValidator).Assembly;
            var validators = assembly.GetExportedTypes().Where(x => x.GetInterfaces().Any(x => x.FullName == "FluentValidation.IValidator"));

            foreach (var item in validators)
            {
                services.AddScoped(typeof(IValidator), item);
            }
            services.AddScoped<IEuromonBooksValidator, EuromonBooksValidator>();
        }
    }
}