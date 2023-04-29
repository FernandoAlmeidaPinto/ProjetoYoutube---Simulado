using Simulado.Dominio;
using Simulado.Fila.Consumidor;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;

namespace Simulado.RelatorioResposta
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumidorBase<EventoDTO<RelatorioSimulado>> _consumidor;
        private readonly IServiceTeste _serviceTeste;

        public Worker(ILogger<Worker> logger, IConsumidorBase<EventoDTO<RelatorioSimulado>> consumidor, IServiceTeste serviceTeste)
        {
            _logger = logger;
            this._consumidor = consumidor;
            this._serviceTeste = serviceTeste;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("Iniciando Consumidor Relatorio");
            this._consumidor.IniciaConsumidor(ExecutaRelatorioSimulado);
        }

        private async Task ExecutaRelatorioSimulado(IEnumerable<EventoDTO<RelatorioSimulado>> eventos)
        {
            foreach (EventoDTO<RelatorioSimulado> evento in eventos)
            {
                await this._serviceTeste.RelatorioSimulado(evento);
            }
        }
    }
}