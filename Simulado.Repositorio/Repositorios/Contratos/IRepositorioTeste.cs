using Simulado.Dominio;

namespace Simulado.Repositorio.Repositorios.Contratos
{
    public interface IRepositorioTeste : IRepositorioBase<Teste>
    {
        Task<bool> AtualizaDificuldade(string testeId, double dificuldade);
    }
}
