using RabbitMQ.Client;
using Simulado.Fila.Publicador;

namespace Simulado.Service.Publicador
{
    public class PublicadorAPI : PublicadorBase
    {
        public PublicadorAPI(IModel channel) : base(channel) { }

        protected override void ExchangeDeclare()
        {
            this._channel.ExchangeDeclare("simulado", ExchangeType.Direct, true, false);
        }
    }
}
