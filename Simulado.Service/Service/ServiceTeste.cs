using AutoMapper;
using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;

namespace Simulado.Service.Service
{
    public class ServiceTeste : ServiceBase<TesteDTO, Teste>, IServiceTeste
    {
        private readonly IServiceQuestao _serviceQuestao;
        private readonly IServiceEstatico<Usuario> _serviceEstatico;
        private readonly IRepositorioTeste _repositorio;
        private readonly IRepositorioBase<RelatorioSimulado> _repositorioRelatorioSimulado;
        private readonly IMapper _autoMapper;
        public ServiceTeste(
            IServiceEstatico<Usuario> serviceEstatico,
            IServiceQuestao serviceQuestao,
            IRepositorioTeste repositorio,
            IRepositorioBase<RelatorioSimulado> repositorioRelatorioSimulado,
            IMapper autoMapper) : base(repositorio, autoMapper)
        {
            this._serviceEstatico = serviceEstatico;
            this._serviceQuestao = serviceQuestao;
            this._repositorio = repositorio;
            this._repositorioRelatorioSimulado = repositorioRelatorioSimulado;
            this._autoMapper = autoMapper;
        }
        public async new Task<bool> Add(TesteDTO testeDTO, string userEmail)
        {
            Usuario? user = (await this._serviceEstatico.GetManyByFilter(new UsuarioFiltro() { Email = userEmail })).FirstOrDefault();

            List<string> questoes =
                (await this._serviceQuestao.GetManyQuestoes(
                    testeDTO.QuantTotalQuestoes - testeDTO.Questoes.Count(), 
                    new QuestaoFiltro() { IsNotIds = testeDTO.Questoes}))
                .Select(q => q._id).ToList();
            foreach (string item in testeDTO.Questoes)
            {
                questoes.Add(item);
            }

            Teste teste = this._autoMapper.Map<Teste>(testeDTO);
            teste.UserId = user!._id;
            teste.Questoes = questoes;

            return await this._repositorio.Add(teste);
        }

        public async Task<RelatorioSimuladoDTOOutput> ResponderSimulador(RelatorioSimuladoDTO relatorioDTO, string userEmail)
        {
            Teste? teste = await this._repositorio.GetByIDAsync(relatorioDTO.Simulado);
            if(teste == null) return new RelatorioSimuladoDTOOutput();
            List<Questao> questoes =
                (await this._serviceQuestao.GetManyByFilter(
                    new QuestaoFiltro() { Ids = relatorioDTO.Respostas.Select(r => r.Questao) })).ToList();

            IEnumerable<Resposta> resposta =
                relatorioDTO.Respostas.Select(
                    r => new Resposta()
                    {
                        Questao = r.Questao,
                        RespostaUsuario = r.RespostaUsuario,
                        RespostaCorreta = questoes.Where(q => q._id == r.Questao).First().AltCorreta,
                    });

            await this._serviceQuestao.ResponderQuestoes(resposta);

            Usuario? user = (await this._serviceEstatico.GetManyByFilter(new UsuarioFiltro() { Email = userEmail })).FirstOrDefault();
            double Aproveitamento = (double)resposta.Select(r => r.RespostaUsuario == r.RespostaCorreta ? 1 : 0).Sum() / teste.QuantTotalQuestoes;
            RelatorioSimulado relatorio = new RelatorioSimulado()
            {
                Simulado = relatorioDTO.Simulado,
                UserId = user!._id,
                Respostas = resposta,
                Aproveitamento = Aproveitamento
            };
            await this._repositorioRelatorioSimulado.Add(relatorio);
            await this.AtualizaDificuldade(teste, Aproveitamento);
            return new RelatorioSimuladoDTOOutput()
            {
                Simulado = relatorioDTO.Simulado,
                Aproveitamento = relatorio.Aproveitamento,
            };
        }

        private async Task AtualizaDificuldade(Teste teste, double aproveitamento)
        {
            int quantRespostas = teste!.Questoes.Count();
            int vezesRespondido = teste.VezesRespondido;
            double dificuldade = 1 - 
                ((1- teste.Dificuldade) * quantRespostas * vezesRespondido + aproveitamento * teste.QuantTotalQuestoes) 
                / (quantRespostas * vezesRespondido + teste.QuantTotalQuestoes);
            await this._repositorio.AtualizaDificuldade(teste._id, dificuldade);
        }
    }
}
