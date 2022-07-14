using Microsoft.Extensions.DependencyInjection;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Bootstrap
{
    public class AuthenticationInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAuthServerAuthenticationRepository, AuthServerAuthenticationRepository>();
            services.AddTransient<ITokenAuthRepository, TokenAuthRepository>();





            services.AddTransient<IUserAuthRepository, UserAuthRepository>();

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
