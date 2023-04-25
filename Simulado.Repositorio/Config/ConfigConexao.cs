using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Simulado.Repositorio.Config
{
    public class ConfigConexao : IConfigConexao
    {
        public ConfigConexao(IOptions<MongoOption> config)
        {
            MongoClient = new MongoClient(config.Value.Conexao);
            MongoDatabase = MongoClient.GetDatabase(config.Value.Database);
        }
        public IMongoDatabase MongoDatabase { get; }

        public IMongoClient MongoClient { get; }
    }
}
