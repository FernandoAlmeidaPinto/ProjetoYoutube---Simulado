using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Simulado.Fila.Consumidor
{
    public abstract class ConsumidorBase<E> : IConsumidorBase<E>
    {
        protected readonly IModel _channel;
        protected readonly string _queue;
        public ConsumidorBase(IModel channel, string queue)
        {
            this._channel = channel;
            this._queue = queue;
            this._channel.BasicQos(0, 1, false);
            this.QueueDeclare();
        }
        public async Task IniciaConsumidor(Func<E, Task> processo)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(this._channel);

            consumer.Received += (model, ea) =>
            {
                try
                {
                    byte[] bytes = ea.Body.ToArray();
                    string mensagem = Encoding.UTF8.GetString(bytes);
                    E? evento = JsonSerializer.Deserialize<E>(mensagem);
                    if (evento != null) processo(evento);
                    this._channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
            this._channel.BasicConsume(this._queue, false, consumer);
        }

        protected abstract void QueueDeclare();
    }
}
