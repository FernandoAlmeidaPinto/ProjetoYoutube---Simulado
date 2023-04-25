using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Simulado.Dominio.Enum;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Simulado.Dominio
{
    public class Questao : Base
    {
        [BsonRequired]
        [BsonElement("enunciado")]
        public string Enunciado { get; set; }

        [BsonRequired]
        [BsonElement("alternativaA")]
        public string AltA { get; set; }

        [BsonRequired]
        [BsonElement("alternativaB")]
        public string AltB { get; set; }

        [BsonRequired]
        [BsonElement("alternativaC")]
        public string AltC { get; set; }

        [BsonRequired]
        [BsonElement("alternativaD")]
        public string AltD { get; set; }

        [BsonRequired]
        [BsonElement("alternativaCorreta")]
        public Alternativa AltCorreta { get; set; }

        [BsonRequired]
        [BsonElement("quantAcertos")]
        public int QuantAcertos { get; set; }

        [BsonRequired]
        [BsonElement("quantRespostas")]
        public int QuantRespostas { get; set; }

        [BsonRequired]
        [BsonElement("userId")]
        public string userID { get; set; }
    }
}
