using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

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
                VirtualHost = "/",
                DispatchConsumersAsync = true,
                Ssl =
                {
                    ServerName = "localhost",
                    Enabled = false
                }
            };

            IConnection conn = null;
            int tentativas = 0;
            while(conn == null && tentativas < 5) 
            { 
                tentativas++;
                Console.WriteLine($"Tentativa {tentativas}");
                try
                {
                    conn = factoty.CreateConnection();
                }
                catch (BrokerUnreachableException bex)
                {
                    Console.WriteLine("BrokerUnreachableException");
                    Console.WriteLine(bex.Message);
                    Thread.Sleep(5000);
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception");
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(5000);
                    continue;
                }
            }

            if(conn == null)
            {
                throw new BrokerUnreachableException(new Exception("Nao foi possivel se conectar após 6 tentativas, verifique o RabbitMQ"));
            }
            return conn;
        }
    }
}
