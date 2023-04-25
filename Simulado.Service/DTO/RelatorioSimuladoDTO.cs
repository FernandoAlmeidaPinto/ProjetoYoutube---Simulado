using MongoDB.Bson.Serialization.Attributes;
using Simulado.Dominio;

namespace Simulado.Service.DTO
{
    public class RelatorioSimuladoDTO
    {
        public string Simulado { get; set; }
        public IEnumerable<RespostaDTO> Respostas { get; set; }
    }
}
