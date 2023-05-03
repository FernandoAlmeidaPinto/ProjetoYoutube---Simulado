using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Simulado.Dominio;
using Simulado.Repositorio.Contexto;
using Simulado.Repositorio.Repositorios;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Test.Contexto;

namespace Simulado.Test.Unitario.User
{
    public class UserRepositorioTest
    {
        private IRepositorioBase<Usuario> _repositorioBase;
        private MongoContextoMock _contexto;
        private string idUser = ObjectId.GenerateNewId().ToString();
        private Mock<ILogger<Usuario>> _loggerUsuario;

        [SetUp]
        public void SetUp()
        {
            Usuario user = new Usuario()
            {
                Email = "test@gmail.com",
                Nome = "test",
                Senha = "test",
                _id = idUser,
            };
            this._loggerUsuario = new();
            this._contexto = new MongoContextoMock(new Mock<IMongoDatabase>());
            this._contexto.IniciaCollection(new List<Usuario>() { user });
            this._repositorioBase = new RepositorioBase<Usuario>(this._contexto, _loggerUsuario.Object);

        }

        [Test]
        public async Task AddOk()
        {
            Usuario user = new Usuario()
            {
                Email = "test@gmail.com",
                Nome = "test",
                Senha = "test",
            };

            bool resultado = await this._repositorioBase.Add(user);
            Assert.IsTrue(resultado);
        }

        [Test]
        public async Task GetByIDOk()
        {
            Usuario? user = await this._repositorioBase.GetByIDAsync(idUser);
            Assert.IsNotNull(user);
            Assert.AreEqual("test@gmail.com", user.Email);
        }

        [Test]
        public async Task AddError()
        {
            Mock<IMongoContexto> _contextoLocal = new();
            IRepositorioBase<Usuario> _repositorioBaseLocal =
                new RepositorioBase<Usuario>(_contextoLocal.Object, _loggerUsuario.Object);

            Usuario user = new Usuario()
            {
                Email = "test@gmail.com",
                Nome = "test",
                Senha = "test",
            };

            bool resultado = await _repositorioBaseLocal.Add(user);

            this._loggerUsuario.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Erro ao Buscar Entidade")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);

            Assert.IsFalse(resultado);
        }
    }
}
