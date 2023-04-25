using AutoMapper;
using Simulado.Dominio;
using Simulado.DTO;
using Simulado.Service.DTO;

namespace Simulado.Service
{
    public class SimuladoProfile : Profile
    {
        public SimuladoProfile()
        {
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<QuestaoDTO, Questao>().ReverseMap();
            CreateMap<TesteDTO, Teste>().ReverseMap();
        }
    }
}
