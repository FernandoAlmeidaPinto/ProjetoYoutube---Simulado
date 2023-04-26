using RabbitMQ.Client;

namespace Simulado.Fila
{
    public static class BaseFila
    {
        public static IConnection IniciaConexao()
        {
            ConnectionFactory factoty = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            IConnection conn = factoty.CreateConnection();
            return conn;
        }
    }
}
