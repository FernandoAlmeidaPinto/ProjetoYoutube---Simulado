using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.Service.DTO;

namespace Simulado.Service.Service.Contratos
{
    public interface IServiceQuestao : IServiceBase<QuestaoDTO, Questao>
    {
        Task<bool> Add(QuestaoDTO questao, string userEmail);
        Task<IEnumerable<Questao>> GetManyQuestoes(int quantidade, IFiltro<Questao>? filtro);
        Task ResponderQuestoes(EventoDTO<RelatorioSimuladoDTO> evento);
    }
}
