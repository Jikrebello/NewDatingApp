using API.Services;
using API.Services.Interfaces;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
