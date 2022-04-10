using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Infra.CrossCutting.IoC.Extensions;
using Agilis.Infra.Data.SqlServer;
using Agilis.Infra.Data.SqlServer.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class DatabaseIoC
    {
        private const string CHAVE_SECRETA = "03DA94BC-B03A-490C-9094-2D7B51D012CC";

        public static IServiceCollection AddDatabaseIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkSqlServer>();

            var connectionString = ObterConnectionString(services);
            services.AddDbContext<AgilisDbContext>(
                optionsBuilder => optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
#if DEBUG
                              .EnableSensitiveDataLogging()
#endif
            );

            return services;
        }

        private static string ObterConnectionString(IServiceCollection services)
        {
            var criptografia = services.Get<ICriptografiaSimetrica>();
            var appSettings = services.Get<IAppSettings>();

            var servidor = appSettings.BancoDados.Servidor;
            var banco = appSettings.BancoDados.Banco;
            var usuario = appSettings.BancoDados.Usuario;
            var senha = criptografia.Decifrar(appSettings.BancoDados.Senha, CHAVE_SECRETA);

            return $"Server={servidor};Database={banco};User Id={usuario};Password={senha};";
        }
    }
}
