using MongoDB.Bson.Serialization.Attributes;
using Simulado.Dominio.Enum;

namespace Simulado.Service.DTO
{
    public class RespostaDTO
    {
        public string Questao { get; set; }
        public Alternativa RespostaUsuario { get; set; }
    }
}
