using Simulado.Fila.Consumidor;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;

namespace Simulado.RelatorioResposta
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumidorBase<EventRelatorioSimuladoDTO> _consumidor;
        private readonly IServiceTeste _serviceTeste;

        public Worker(ILogger<Worker> logger, IConsumidorBase<EventRelatorioSimuladoDTO> consumidor, IServiceTeste serviceTeste)
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

        private async Task ExecutaRelatorioSimulado(IEnumerable<EventRelatorioSimuladoDTO> eventos)
        {
            foreach (EventRelatorioSimuladoDTO evento in eventos)
            {
                await this._serviceTeste.ResponderSimulador(evento.relatorio, evento.emailUser);
            }
        }
    }
}