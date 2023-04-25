using MongoDB.Driver;
using Simulado.Repositorio.Contexto;
using Simulado.Repositorio.Repositorios.Contratos;

namespace Simulado.Repositorio.Repositorios
{
    public class RepositorioBase<D> : RepositorioEstatico<D>,  IRepositorioBase<D> where D : class
    {
        private readonly IMongoCollection<D> _collection;
        public RepositorioBase(IMongoContexto contexto) : base(contexto)
        {
            this._collection = contexto.GetCollection<D>();
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
                return false;
            }
        }
    }
}
