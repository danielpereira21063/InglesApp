using InglesApp.Data.Repositories;
using InglesApp.Data.Transaction;
using InglesApp.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InglesApp.Data.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AdicionarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IVocabularioRepository, VocabularioRepository>();
        }
    }
}
