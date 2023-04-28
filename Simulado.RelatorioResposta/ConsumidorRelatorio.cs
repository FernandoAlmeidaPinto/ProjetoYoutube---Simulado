﻿using RabbitMQ.Client;
using Simulado.Fila.Consumidor;
using Simulado.Service.DTO;

namespace Simulado.RelatorioResposta
{
    public class ConsumidorRelatorio : ConsumidorBase<EventRelatorioSimuladoDTO>
    {
        private const string queueName = "filaSimulado";

        private const ushort _prefetchSize = 500;
        public ConsumidorRelatorio(IModel channel) : base(channel, queueName, _prefetchSize) {}
        protected override void QueueDeclare()
        {
            this._channel.QueueDeclare(queueName, true, false, false);
        }
    }
}
