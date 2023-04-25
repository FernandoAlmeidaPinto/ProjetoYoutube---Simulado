using MongoDB.Bson.Serialization.Attributes;

namespace Simulado.Service.DTO
{
    public class TesteDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QuantTotalQuestoes { get; set; }
        public IEnumerable<string> Questoes { get; set; }
    }
}
