using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Simulado.Dominio.Const;

namespace Simulado.Dominio
{
    public class Usuario : Base
    {

        [BsonRequired]
        [BsonElement("nome")]
        public string Nome { get; set; }

        [BsonRequired]
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonRequired]
        [BsonElement("password")]
        public string Senha { get; set; }

        [BsonRequired]
        [BsonElement("role")]
        public string Role { get; set; } = Permissao.Estudante;
    }
}
