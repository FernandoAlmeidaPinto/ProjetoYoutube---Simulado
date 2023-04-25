using MongoDB.Bson.Serialization.Attributes;

namespace Simulado.Dominio
{
    public class RelatorioSimulado : Base
    {
        [BsonRequired]
        [BsonElement("simuladoId")]
        public string Simulado { get; set; }

        [BsonRequired]
        [BsonElement("userID")]
        public string UserId { get; set; }

        [BsonRequired]
        [BsonElement("aproveitamento")]
        public double Aproveitamento { get; set; }

        [BsonRequired]
        [BsonElement("questoes")]
        public IEnumerable<Resposta> Respostas { get; set; }
    }
}
