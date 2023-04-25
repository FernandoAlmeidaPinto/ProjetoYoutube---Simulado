using Simulado.Dominio;
using Simulado.Dominio.Filtros;

namespace Simulado.Repositorio.Repositorios.Contratos
{
    public interface IRepositorioQuestao : IRepositorioBase<Questao>
    {
        Task<IEnumerable<Questao>> GetQuestaoByQuant(int quantidade, IFiltro<Questao>? filtro = null);
        Task<bool> ResponderQuestoes(IEnumerable<Resposta> respostas);
    }
}
