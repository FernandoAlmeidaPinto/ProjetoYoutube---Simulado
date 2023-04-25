using Simulado.Dominio.Filtros;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.Service.Contratos;

namespace Simulado.Service.Service
{
    public class ServiceEstatico<D> : IServiceEstatico<D> where D : class
    {
        private readonly IRepositorioEstatico<D> _repositorio;
        public ServiceEstatico(IRepositorioEstatico<D> repositorio)
        {
            this._repositorio = repositorio;
        }
        public async Task<D?> GetById(string id)
        {
            return await this._repositorio.GetByIDAsync(id);
        }

        public Task<IEnumerable<D>> GetManyByFilter(IFiltro<D> filtro)
        {
            return this._repositorio.GetManyByFilter(filtro);
        }
    }
}
