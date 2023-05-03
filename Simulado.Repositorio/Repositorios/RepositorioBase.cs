using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Simulado.Repositorio.Contexto;
using Simulado.Repositorio.Repositorios.Contratos;

namespace Simulado.Repositorio.Repositorios
{
    public class RepositorioBase<D> : RepositorioEstatico<D>,  IRepositorioBase<D> where D : class
    {
        private readonly IMongoCollection<D> _collection;
        private readonly ILogger<D> _logger;
        public RepositorioBase(IMongoContexto contexto, ILogger<D> logger) : base(contexto)
        {
            this._collection = contexto.GetCollection<D>();
            this._logger = logger;
        }
        public async Task<bool> Add(D entidade)
        {
            try
            {
                await this._collection.InsertOneAsync(entidade);
                return true;
            }
            catch (Exception)
            {
                this._logger.LogError("Erro ao Buscar Entidade");
                return false;
            }
        }
    }
}
