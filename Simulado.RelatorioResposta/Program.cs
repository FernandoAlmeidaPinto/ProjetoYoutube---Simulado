using AutoMapper;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Simulado.Dominio;
using Simulado.Fila;
using Simulado.Fila.Consumidor;
using Simulado.RelatorioResposta;
using Simulado.Repositorio;
using Simulado.Repositorio.Config;
using Simulado.Repositorio.Contexto;
using Simulado.Repositorio.Repositorios;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.ConfigMapper;
using Simulado.Service.DTO;
using Simulado.Service.Service;
using Simulado.Service.Service.Contratos;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddHostedService<Worker>();
        services.AddSingleton<IConnection>(s => BaseFila.IniciaConexao());
        services.AddTransient<IModel>(s => s.GetRequiredService<IConnection>().CreateModel());
        services.Configure<MongoOption>(opt =>
        {
            opt.Database = configuration.GetSection("MongoSettings:Database").Value;
            opt.Conexao = configuration.GetSection("MongoSettings:Conexao").Value;
        });
        services.AddSingleton<IConfigConexao, ConfigConexao>();
        services.AddSingleton<IMongoContexto, MongoContexto>();

        services.AddSingleton<IConsumidorBase<EventRelatorioSimuladoDTO>, ConsumidorRelatorio>();

        services.AddTransient<IServiceTeste, ServiceTeste>();
        services.AddTransient<IServiceQuestao, ServiceQuestao>();
        services.AddTransient<IServiceEstatico<Usuario>, ServiceEstatico<Usuario>>();
        services.AddTransient<IRepositorioEstatico<Usuario>, RepositorioEstatico<Usuario>>();
        services.AddTransient<IRepositorioTeste, RepositorioTeste>();
        services.AddTransient<IRepositorioQuestao, RepositorioQuestao>();
        services.AddTransient<IRepositorioBase<RelatorioSimulado>, RepositorioBase<RelatorioSimulado>>();
        services.AddTransient(s => new ConfigMapper().AutoMapper);

    })
    .Build();

host.Run();
