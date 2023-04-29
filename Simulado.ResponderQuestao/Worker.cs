using Simulado.Fila.Consumidor;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;

namespace Simulado.ResponderQuestao
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumidorBase<EventoDTO<RelatorioSimuladoDTO>> _consumidor;
        private readonly IServiceQuestao _serviceQuestao;

        public Worker(ILogger<Worker> logger, IConsumidorBase<EventoDTO<RelatorioSimuladoDTO>> consumidor, IServiceQuestao serviceQuestao)
        {
            _logger = logger;
            this._consumidor = consumidor;
            this._serviceQuestao = serviceQuestao;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("Iniciando Consumidor Relatorio");
            this._consumidor.IniciaConsumidor(ExecutaRelatorioSimulado);
        }

        private async Task ExecutaRelatorioSimulado(IEnumerable<EventoDTO<RelatorioSimuladoDTO>> eventos)
        {
            foreach (EventoDTO<RelatorioSimuladoDTO> evento in eventos)
            {
                await this._serviceQuestao.ResponderQuestoes(evento);
            }
        }
    }
}