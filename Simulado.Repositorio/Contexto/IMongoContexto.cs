using MongoDB.Driver;

namespace Simulado.Repositorio.Contexto
{
    public interface IMongoContexto
    {
        IMongoCollection<D> GetCollection<D>();
    }
}
