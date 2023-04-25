namespace Simulado.Repositorio.Repositorios.Contratos
{
    public interface IRepositorioBase<D> : IRepositorioEstatico<D> where D : class
    {
        Task<bool> Add(D entidade);
    }
}
