using Simulado.Configuracao;
using Simulado.Dominio;
using Simulado.Repositorio.Repositorios;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.Service;
using Simulado.Service.Service.Contratos;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigurationManager configuration = builder.Configuration;

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddServices();
        builder.Services.AddRepositorios();
        builder.Services.AddContexto(configuration);
        builder.Services.AddJwtBearerSimulado();

        builder.Services.AddRabbitMQ();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}