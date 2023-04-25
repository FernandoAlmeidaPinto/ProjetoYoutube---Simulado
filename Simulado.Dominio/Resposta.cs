using MongoDB.Bson.Serialization.Attributes;
using Simulado.Dominio.Enum;

namespace Simulado.Dominio
{
    public class Resposta
    {
        [BsonRequired]
        [BsonElement("questao")]
        public string Questao { get; set; }

        [BsonRequired]
        [BsonElement("respostaUsuario")]
        public Alternativa RespostaUsuario { get; set; }

        [BsonRequired]
        [BsonElement("respostaCorreta")]
        public Alternativa RespostaCorreta { get; set; }
    }
}
