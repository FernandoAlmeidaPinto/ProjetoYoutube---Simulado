using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Simulado.Dominio
{
    public class Teste : Base
    {
        [BsonRequired]
        [BsonElement("nome")]
        public string Nome { get; set; }

        [BsonRequired]
        [BsonElement("descricao")]
        public string Descricao { get; set; }

        [BsonRequired]
        [BsonElement("dificuldade")]
        public double Dificuldade { get; set; }

        [BsonRequired]
        [BsonElement("quantTotalQuestoes")]
        public int QuantTotalQuestoes { get; set; }

        [BsonRequired]
        [BsonElement("vezesRespondido")]
        public int VezesRespondido { get; set; }

        [BsonRequired]
        [BsonElement("questoes")]
        public IEnumerable<string> Questoes { get; set; }

        [BsonRequired]
        [BsonElement("userID")]
        public string UserId { get; set; }
    }
}
