using RabbitMQ.Client;
using Simulado.Dominio;
using Simulado.Fila.Consumidor;
using Simulado.Service.DTO;

namespace Simulado.RelatorioResposta
{
    public class ConsumidorRelatorio : ConsumidorBase<EventoDTO<RelatorioSimulado>>
    {
        private const string queueName = "filaSimulado";

        private const ushort _prefetchSize = 1;
        public ConsumidorRelatorio(IModel channel) : base(channel, queueName, _prefetchSize) {}
        protected override void QueueDeclare()
        {
            this._channel.QueueDeclare(queueName, true, false, false);
        }
    }
}
