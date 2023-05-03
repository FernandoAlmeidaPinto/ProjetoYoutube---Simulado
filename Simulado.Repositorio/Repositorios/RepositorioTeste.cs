using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Simulado.Dominio;
using Simulado.Dominio.Const;
using Simulado.Repositorio.Contexto;
using Simulado.Repositorio.Repositorios.Contratos;

namespace Simulado.Repositorio.Repositorios
{
    public class RepositorioTeste : RepositorioBase<Teste>, IRepositorioTeste
    {
        private readonly IMongoCollection<Teste> _collection;
        private readonly ILogger<Teste> _logger;
        public RepositorioTeste(IMongoContexto contexto, ILogger<Teste> logger) : base(contexto, logger)
        {
            this._collection = contexto.GetCollection<Teste>();
            this._logger = logger;
        }
        public async Task<bool> AtualizaDificuldade(string testeId, double dificuldade)
        {
            FilterDefinition<Teste> filter = Builders<Teste>.Filter.Eq("_id", testeId);
            UpdateDefinition<Teste> update =
                Builders<Teste>.Update
                .Set("dificuldade", dificuldade)
                .Inc("vezesRespondido", 1);

            UpdateResult result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}
