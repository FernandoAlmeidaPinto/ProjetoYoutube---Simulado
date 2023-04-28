namespace Simulado.Fila.Consumidor
{
    public interface IConsumidorBase<E>
    {
        void IniciaConsumidor(Func<IEnumerable<E>, Task> processo);
    }
}
