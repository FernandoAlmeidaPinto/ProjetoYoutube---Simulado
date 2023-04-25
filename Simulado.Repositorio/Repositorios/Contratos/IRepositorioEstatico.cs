using Simulado.Dominio.Filtros;

namespace Simulado.Repositorio.Repositorios.Contratos
{
    public interface IRepositorioEstatico<D> where D : class
    {
        Task<D?> GetByIDAsync(string id);

        Task<IEnumerable<D>> GetManyByFilter(IFiltro<D> filter);
    }
}
