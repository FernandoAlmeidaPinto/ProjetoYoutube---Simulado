using AutoMapper;
using Moq;
using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.DTO;
using Simulado.Service.Service;
using Simulado.Service.Service.Contratos;

namespace Simulado.Test.Unitario.Simulado
{
    public class SimuladoServiceBaseTest
    {
        private IServiceTeste _serviceTeste;
        private Mock<IServiceEstatico<Usuario>> _serviceEstaticoUsuario;
        private Mock<IServiceQuestao> _serviceQuestao;
        private Mock<IRepositorioTeste> _repositorioTeste;
        private Mock<IRepositorioBase<RelatorioSimulado>> _repositorioBaseRelatorio;
        private Mock<IMapper> _mapperBase;
        [SetUp] 
        public void Init() 
        {
            this._serviceEstaticoUsuario = new();
            this._serviceQuestao = new();
            this._repositorioTeste = new();
            this._repositorioBaseRelatorio = new();
            this._mapperBase = new();

            this._serviceTeste = new ServiceTeste(
                this._serviceEstaticoUsuario.Object, 
                this._serviceQuestao.Object, 
                this._repositorioTeste.Object, 
                this._repositorioBaseRelatorio.Object, 
                this._mapperBase.Object);
        }

        [Test]
        public async Task AddOk()
        {
            Usuario usuario = new();
            List<Usuario> lista = new List<Usuario>() { usuario };

            Questao questao = new Questao();
            questao._id = "idTestQuestao";
            List<Questao> questoes = new List<Questao>() { questao };

            TesteDTO testeDTO = new TesteDTO()
            {
                Nome = "NomeTesteTest",
                Descricao = "Descricao Qualquer",
                QuantTotalQuestoes = 2,
                Questoes = new List<string>(){ "123455dd" }
            };
            Teste teste = new Teste()
            {
                Nome = testeDTO.Nome,
                Descricao = testeDTO.Descricao,
                QuantTotalQuestoes = 2,
                Questoes = testeDTO.Questoes
            };

            this._serviceEstaticoUsuario.Setup(s => s.GetManyByFilter(It.IsAny<UsuarioFiltro>())).ReturnsAsync(lista);
            this._serviceQuestao.Setup(s => s.GetManyQuestoes(It.IsAny<int>(), It.IsAny<QuestaoFiltro>())).ReturnsAsync(questoes);
            this._mapperBase.Setup(m => m.Map<Teste>(It.IsAny<TesteDTO>())).Returns(teste);
            this._repositorioTeste.Setup(r => r.Add(It.IsAny<Teste>())).ReturnsAsync(true);

            bool resultado = await this._serviceTeste.Add(testeDTO, "fernando@gmail.com");
            Assert.IsTrue(resultado);
            Assert.AreEqual(2, teste.Questoes.Count());
            Assert.AreEqual("idTestQuestao", teste.Questoes.First());
            Assert.AreEqual("123455dd", teste.Questoes.Last());
        }
    }
}
