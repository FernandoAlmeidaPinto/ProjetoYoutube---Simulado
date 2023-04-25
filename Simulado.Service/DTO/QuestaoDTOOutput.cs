using Simulado.Dominio.Enum;

namespace Simulado.Service.DTO
{
    public class QuestaoDTOOutput
    {
        public string id { get; set; }
        public string Enunciado { get; set; }
        public string AltA { get; set; }
        public string AltB { get; set; }
        public string AltC { get; set; }
        public string AltD { get; set; }
        public double Dificuldade { get; set; }
    }
}
