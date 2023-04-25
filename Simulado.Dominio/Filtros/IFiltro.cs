using MongoDB.Driver;

namespace Simulado.Dominio.Filtros
{
    public interface IFiltro<D>
    {
        FilterDefinition<D> GetFiltro();
    }
}
