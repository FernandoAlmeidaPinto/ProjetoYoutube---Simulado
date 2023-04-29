using Simulado.Dominio;
using Simulado.Service.DTO;

namespace Simulado.Service.Service.Contratos
{
    public interface IServiceTeste : IServiceBase<TesteDTO, Teste>
    {
        Task<bool> Add(TesteDTO teste, string userEmail);
        //Task<RelatorioSimuladoDTOOutput> ResponderSimulador(RelatorioSimuladoDTO relatorioDTO, string userEmail);
        Task RelatorioSimulado(EventoDTO<RelatorioSimulado> evento);
    }
}
