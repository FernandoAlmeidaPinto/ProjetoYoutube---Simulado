using AutoMapper;
using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.Fila.Publicador;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;

namespace Simulado.Service.Service
{
    public class ServiceQuestao : ServiceBase<QuestaoDTO, Questao>, IServiceQuestao
    {
        private readonly IServiceEstatico<Usuario> _serviceEstatico;
        private readonly IRepositorioQuestao _repositorio;
        private readonly IPublicadorBase _publicador;
        private readonly IMapper _autoMapper;
        public ServiceQuestao(
            IServiceEstatico<Usuario> _serviceEstatico,
            IRepositorioQuestao repositorio,
            IMapper autoMapper,
            IPublicadorBase publicador = null) : base(repositorio, autoMapper)
        {
            this._serviceEstatico = _serviceEstatico;
            this._repositorio = repositorio;
            this._publicador = publicador;
            this._autoMapper = autoMapper;
        }

        public async new Task<bool> Add(QuestaoDTO dto, string userEmail)
        {
            Questao dominio = this._autoMapper.Map<Questao>(dto);
            Usuario? user = (await this._serviceEstatico.GetManyByFilter(new UsuarioFiltro() { Email = userEmail })).FirstOrDefault();
            dominio.userID = user!._id;
            return await this._repositorio.Add(dominio);
        }

        public async Task ResponderQuestoes(EventoDTO<RelatorioSimuladoDTO> evento)
        {
            List<Questao> questoes =
                (await this._repositorio.GetManyByFilter(
                    new QuestaoFiltro() { Ids = evento.Relatorio.Respostas.Select(r => r.Questao) })).ToList();

            IEnumerable<Resposta> resposta =
                evento.Relatorio.Respostas.Select(
                    r => new Resposta()
                    {
                        Questao = r.Questao,
                        RespostaUsuario = r.RespostaUsuario,
                        RespostaCorreta = questoes.Where(q => q._id == r.Questao).First().AltCorreta,
                    });

            await this.ResponderQuestoes(resposta);
            this._publicador.PublicaMensagem(
                "simulado",
                "salvaRelatorio",
                this._publicador.ConverteMensagem(new EventoDTO<RelatorioSimulado>()
                {
                    Relatorio = new RelatorioSimulado()
                    {
                        Respostas = resposta,
                        Simulado = evento.Relatorio.Simulado
                    },
                    EmailUser = evento.EmailUser,
                }));
        }
        public async Task<IEnumerable<Questao>> GetManyQuestoes(int quantidade, IFiltro<Questao>? filtro)
        {
            return await this._repositorio.GetQuestaoByQuant(quantidade, filtro);
        }
        private async Task<bool> ResponderQuestoes(IEnumerable<Resposta> respostas)
        {
            return await this._repositorio.ResponderQuestoes(respostas);
        }
    }
}
