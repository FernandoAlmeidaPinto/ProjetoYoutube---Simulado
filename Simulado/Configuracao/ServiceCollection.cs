using Simulado.Dominio;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Repositorio.Repositorios;
using Simulado.Service.Service.Contratos;
using Simulado.Service.Service;
using Simulado.Repositorio;
using Simulado.Repositorio.Config;
using Simulado.Repositorio.Contexto;
using Simulado.DTO;
using Simulado.Service.ConfigCript;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Simulado.Service.DTO;

namespace Simulado.Configuracao
{
    public static class ServiceCollection
    {

        public static IServiceCollection AddContexto(this IServiceCollection services, IConfiguration configuration) 
        {

            services.Configure<MongoOption>(opt =>
            {
                opt.Database = configuration.GetSection("MongoSettings:Database").Value;
                opt.Conexao = configuration.GetSection("MongoSettings:Conexao").Value;
            });
            services.AddScoped<IConfigConexao, ConfigConexao>();
            services.AddScoped<IMongoContexto, MongoContexto>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceEstatico<Usuario>, ServiceEstatico<Usuario>>();
            services.AddScoped<IServiceBase<UsuarioDTO, Usuario>, ServiceBase<UsuarioDTO, Usuario>>();
            services.AddScoped<IServiceQuestao, ServiceQuestao>();
            services.AddScoped<IServiceUser, ServiceUser>();
            services.AddScoped<IServiceTeste, ServiceTeste>();
            services.AddScoped<ServiceToken>();

            return services;
        }

        public static IServiceCollection AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioEstatico<Usuario>, RepositorioEstatico<Usuario>>();

            services.AddScoped<IRepositorioBase<Usuario>, RepositorioBase<Usuario>>();
            services.AddScoped<IRepositorioBase<Questao>, RepositorioBase<Questao>>();
            services.AddScoped<IRepositorioTeste, RepositorioTeste>();
            services.AddScoped<IRepositorioBase<RelatorioSimulado>, RepositorioBase<RelatorioSimulado>>();

            services.AddScoped<IRepositorioQuestao, RepositorioQuestao>();

            return services;
        }

        public static IServiceCollection AddJwtBearerSimulado(this IServiceCollection services)
        {
            byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
