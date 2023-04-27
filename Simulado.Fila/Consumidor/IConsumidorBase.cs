namespace Simulado.Fila.Consumidor
{
    public interface IConsumidorBase<E>
    {
        Task IniciaConsumidor(Func<E, Task> processo);
    }
}
