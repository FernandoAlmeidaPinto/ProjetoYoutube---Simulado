namespace Simulado.Fila.Publicador
{
    public interface IPublicadorBase
    {
        void PublicaMensagem(string exchange, string routingKey, ReadOnlyMemory<byte> body);

        ReadOnlyMemory<byte> ConverteMensagem(object item);
    }
}
