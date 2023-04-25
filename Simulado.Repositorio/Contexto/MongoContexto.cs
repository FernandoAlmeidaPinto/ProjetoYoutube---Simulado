using MongoDB.Driver;
using Simulado.Repositorio.Config;

namespace Simulado.Repositorio.Contexto
{
    public class MongoContexto : IMongoContexto
    {
        private readonly IConfigConexao _config;
        public MongoContexto(IConfigConexao config)
        {
            this._config = config;
        }
        public IMongoCollection<D> GetCollection<D>()
        {
            return this._config.MongoDatabase.GetCollection<D>(typeof(D).Name);
        }
    }
}
