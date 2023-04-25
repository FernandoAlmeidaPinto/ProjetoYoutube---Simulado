using MongoDB.Driver;

namespace Simulado.Repositorio.Config
{
    public interface IConfigConexao
    {
        IMongoDatabase MongoDatabase { get; }
        IMongoClient MongoClient { get; }
    }
}
