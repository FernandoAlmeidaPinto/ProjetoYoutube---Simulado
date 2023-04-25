using AutoMapper;
using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;

namespace Simulado.Service.Service
{
    public class ServiceQuestao : ServiceBase<QuestaoDTO, Questao>, IServiceQuestao
    {
        private readonly IServiceEstatico<Usuario> _serviceEstatico;
        private readonly IRepositorioQuestao _repositorio;
        private readonly IMapper _autoMapper;
        public ServiceQuestao(
            IServiceEstatico<Usuario> _serviceEstatico,
            IRepositorioQuestao repositorio, 
            IMapper autoMapper) : base(repositorio, autoMapper)
        {
            this._repositorio = repositorio;
            this._autoMapper = autoMapper;
            this._serviceEstatico = _serviceEstatico;
        }

        public async new Task<bool> Add(QuestaoDTO dto, string userEmail)
        {
            Questao dominio = this._autoMapper.Map<Questao>(dto);
            Usuario? user = (await this._serviceEstatico.GetManyByFilter(new UsuarioFiltro() { Email = userEmail })).FirstOrDefault();
            dominio.userID = user!._id;
            return await this._repositorio.Add(dominio);
        }

        public async Task<IEnumerable<Questao>> GetManyQuestoes(int quantidade, IFiltro<Questao>? filtro)
        {
            return await this._repositorio.GetQuestaoByQuant(quantidade, filtro);
        }
        public async Task<bool> ResponderQuestoes(IEnumerable<Resposta> respostas)
        {
            return await this._repositorio.ResponderQuestoes(respostas);
        }
    }
}
