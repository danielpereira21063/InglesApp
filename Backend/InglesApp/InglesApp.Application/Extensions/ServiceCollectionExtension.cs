using InglesApp.Application.Services;
using InglesApp.Application.Services.Account;
using InglesApp.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InglesApp.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AdicionarServicos(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IVocabularioService, VocabularioService>();
            services.AddScoped<IPraticaService, PraticaService>();
        }
    }
}
