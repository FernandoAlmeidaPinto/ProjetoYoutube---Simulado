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
        private readonly ushort _prefetchSize;
        private List<E> _items;
        public ConsumidorBase(IModel channel, string queue, ushort prefetchSize)
        {
            this._channel = channel;
            this._queue = queue;
            this._items = new List<E>();
            this._prefetchSize = prefetchSize;
            this._channel.BasicQos(0, prefetchSize, false);
            this.QueueDeclare();
        }
        public void IniciaConsumidor(Func<IEnumerable<E>, Task> processo)
        {
            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(this._channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    byte[] bytes = ea.Body.ToArray();
                    string mensagem = Encoding.UTF8.GetString(bytes);
                    E? evento = JsonSerializer.Deserialize<E>(mensagem);
                    if (evento != null)
                    {
                        this._items.Add(evento);
                        if(this._items.Count == this._prefetchSize)
                        {
                            await processo.Invoke(this._items);
                            this._items.Clear();
                            this._channel.BasicAck(ea.DeliveryTag, false);
                        }
                    }
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
