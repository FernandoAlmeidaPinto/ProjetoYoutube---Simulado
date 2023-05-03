using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SharpCompress.Common;
using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.Repositorio.Contexto;
using Simulado.Repositorio.Repositorios.Contratos;

namespace Simulado.Repositorio.Repositorios
{
    public class RepositorioQuestao : RepositorioBase<Questao>, IRepositorioQuestao
    {
        private readonly IMongoCollection<Questao> _collection;
        private readonly ILogger<Questao> _logger;
        public RepositorioQuestao(IMongoContexto contexto, ILogger<Questao> logger) : base(contexto, logger)
        {
            this._collection = contexto.GetCollection<Questao>();
            this._logger = logger;
        }
        public async Task<IEnumerable<Questao>> GetQuestaoByQuant(int quantidade, IFiltro<Questao>? filtro = null)
        {
            FilterDefinition<Questao> fieldDefinition = filtro != null ? filtro.GetFiltro() : Builders<Questao>.Filter.Empty;
            return await this.GetQuestoes(quantidade, fieldDefinition);
        }

        public async Task<bool> ResponderQuestoes(IEnumerable<Resposta> respostas)
        {
            var bulkList = new List<WriteModel<Questao>>();
            foreach (var resposta in respostas)
            {
                FilterDefinition<Questao> filter = Builders<Questao>.Filter.Eq(x => x._id, resposta.Questao);
                UpdateDefinition<Questao> update = 
                    Builders<Questao>.Update
                    .Inc("quantRespostas", 1)
                    .Inc("quantAcertos", resposta.RespostaUsuario == resposta.RespostaCorreta ? 1 : 0);
                bulkList.Add(new UpdateOneModel<Questao>(filter, update) { IsUpsert = true });
            }
            BulkWriteResult result = await this._collection.BulkWriteAsync(bulkList);
            return result.ModifiedCount == respostas.Count();
        }

        private async Task<IEnumerable<Questao>> GetQuestoes(int quantidade, FilterDefinition<Questao> filterDefinition)
        {
            return await this._collection.Find(filterDefinition).Limit(quantidade).ToListAsync();
        }
    }
}
