using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Dominio.Filtros
{
    public class QuestaoFiltro : IFiltro<Questao>
    {
        public IEnumerable<string>? Ids { get; set; }
        public IEnumerable<string>? IsNotIds { get; set; }
        public FilterDefinition<Questao> GetFiltro()
        {
            List<FilterDefinition<Questao>> listaFiltro = new();

            if(Ids != null && Ids.Any())
            {
                listaFiltro.Add(Builders<Questao>.Filter.In("_id", Ids));
            }

            if (IsNotIds != null && IsNotIds.Any())
            {
                foreach (string id in IsNotIds)
                {
                    listaFiltro.Add(Builders<Questao>.Filter.Ne("_id", id));
                }
            }

            if (!listaFiltro.Any())
            {
                return Builders<Questao>.Filter.Empty;
            }
            return Builders<Questao>.Filter.And(listaFiltro);
        }
    }
}
