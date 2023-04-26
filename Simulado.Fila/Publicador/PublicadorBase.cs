using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Simulado.Fila.Publicador
{
    public abstract class PublicadorBase : IPublicadorBase
    {
        protected readonly IModel _channel;
        public PublicadorBase(IModel channel)
        {
            this._channel = channel;
            this.ExchangeDeclare();
        }
        public void PublicaMensagem(string exchange, string routingKey, ReadOnlyMemory<byte> body)
        {
            this._channel.BasicPublish(exchange, routingKey, null, body);
        }

        public ReadOnlyMemory<byte> ConverteMensagem(object item)
        {
            string conteudo = JsonSerializer.Serialize(item);
            byte[] bytes = Encoding.UTF8.GetBytes(conteudo);

            return new ReadOnlyMemory<byte>(bytes);
        }

        protected abstract void ExchangeDeclare();
    }
}
