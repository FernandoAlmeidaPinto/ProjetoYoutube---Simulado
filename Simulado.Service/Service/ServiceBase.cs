using AutoMapper;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.Service.Contratos;

namespace Simulado.Service.Service
{
    public class ServiceBase<DTO, D> : ServiceEstatico<D>, IServiceBase<DTO, D> where D : class
    {
        private readonly IRepositorioBase<D> _repositorioBase;
        private readonly IMapper _autoMapper;
        public ServiceBase(IRepositorioBase<D> repositorioBase, IMapper autoMapper) : base(repositorioBase)
        {
            this._repositorioBase = repositorioBase;
            this._autoMapper = autoMapper;
        }

        public async virtual Task<bool> Add(DTO entidade)
        {
            D dominio = this._autoMapper.Map<D>(entidade);
            return await this._repositorioBase.Add(dominio);
        }
    }
}
