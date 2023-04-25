using MongoDB.Bson;
using MongoDB.Driver;
using Simulado.Dominio.Filtros;
using Simulado.Repositorio.Contexto;
using Simulado.Repositorio.Repositorios.Contratos;

namespace Simulado.Repositorio.Repositorios
{
    public class RepositorioEstatico<D> : IRepositorioEstatico<D> where D : class
    {
        private readonly IMongoCollection<D> _collection;
        public RepositorioEstatico(IMongoContexto contexto)
        {
            this._collection = contexto.GetCollection<D>();
        }
        public async Task<D?> GetByIDAsync(string id)
        {
            return (await this._collection.FindAsync(Builders<D>.Filter.Eq("_id", ObjectId.Parse(id)))).FirstOrDefault();
        }

        public async Task<IEnumerable<D>> GetManyByFilter(IFiltro<D> filter)
        {
            return (await this._collection.FindAsync(Builders<D>.Filter.And(filter.GetFiltro()))).ToEnumerable();
        }
    }
}
