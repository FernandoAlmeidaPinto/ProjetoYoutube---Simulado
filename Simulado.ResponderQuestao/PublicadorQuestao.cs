using RabbitMQ.Client;
using Simulado.Fila.Publicador;

namespace Simulado.ResponderQuestao
{
    public class PublicadorQuestao : PublicadorBase
    {
        public PublicadorQuestao(IModel channel) : base(channel) {}
        protected override void ExchangeDeclare()
        {
            this._channel.ExchangeDeclare("simulado", ExchangeType.Direct, true, false);
        }
    }
}
