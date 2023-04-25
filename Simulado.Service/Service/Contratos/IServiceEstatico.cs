using Simulado.Dominio.Filtros;

namespace Simulado.Service.Service.Contratos
{
    public interface IServiceEstatico<D> where D : class
    { 
        Task<D?> GetById(string id);

        Task<IEnumerable<D>> GetManyByFilter(IFiltro<D> filtro);
    }
}
