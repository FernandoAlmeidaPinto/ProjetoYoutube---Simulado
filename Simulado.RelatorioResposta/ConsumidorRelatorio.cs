using RabbitMQ.Client;
using Simulado.Fila.Consumidor;
using Simulado.Service.DTO;

namespace Simulado.RelatorioResposta
{
    public class ConsumidorRelatorio : ConsumidorBase<EventRelatorioSimuladoDTO>
    {
        private readonly static string queueName = "filaSimulado";
        public ConsumidorRelatorio(IModel channel) : base(channel, queueName) {}
        protected override void QueueDeclare()
        {
            this._channel.QueueDeclare(queueName, true, false, false);
        }
    }
}
