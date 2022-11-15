using EuromonBooks.Abstractions.Services;
using EuromonBooks.Abstractions.Services.JwtService;
using EuromonBooks.Domain.Abstractions.Services;
using EuromonBooks.Domain.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EuromonBooks.Domain
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure(ConfigureJwtAuthenticationServicesAndOptions(services, configuration));

            return services.AddScoped<IJwtService, JwtService>()
                .AddScoped<ILoginService, LoginService>()
                .AddScoped<IAccountService, AccountService>();
        }

        private static Action<JwtOptions> ConfigureJwtAuthenticationServicesAndOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("JWT");
            return options => section.Bind(options);
        }
    }
}