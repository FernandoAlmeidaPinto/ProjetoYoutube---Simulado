using RabbitMQ.Client;
using Simulado.Fila.Consumidor;
using Simulado.Service.DTO;

namespace Simulado.ResponderQuestao
{
    public class ConsumidorQuestao : ConsumidorBase<EventoDTO<RelatorioSimuladoDTO>>
    {
        private const string queueName = "filaQuestao";

        private const ushort _prefetchSize = 1;
        public ConsumidorQuestao(IModel channel) : base(channel, queueName, _prefetchSize) { }
        protected override void QueueDeclare()
        {
            this._channel.QueueDeclare(queueName, true, false, false);
        }
    }
}
