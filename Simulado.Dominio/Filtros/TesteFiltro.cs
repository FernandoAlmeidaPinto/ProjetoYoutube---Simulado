using MongoDB.Driver;

namespace Simulado.Dominio.Filtros
{
    public class TesteFiltro : IFiltro<Teste>
    {
        public FilterDefinition<Teste> GetFiltro()
        {
            return Builders<Teste>.Filter.Empty;
        }
    }
}
