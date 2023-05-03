using MongoDB.Driver;
using Moq;
using Simulado.Repositorio.Config;
using Simulado.Repositorio.Contexto;

namespace Simulado.Test.Contexto
{
    public class MongoContextoMock : MongoContexto
    {
        private Mock<IMongoDatabase> _mongoDatabaseMock;
        public MongoContextoMock(Mock<IMongoDatabase> mongoDatabaseMock) : base(GetConfig(mongoDatabaseMock))
        {
            this._mongoDatabaseMock = mongoDatabaseMock;
        }

        public void IniciaCollection<D>(IEnumerable<D> listaElementos)
        {
            Mock<IMongoCollection<D>> mockCollection = new Mock<IMongoCollection<D>>();
            Mock<IAsyncCursor<D>> mockCursor = new Mock<IAsyncCursor<D>>();

            mockCursor.Setup(_ => _.Current).Returns(listaElementos);
            mockCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));
            mockCollection.Setup(co => co.FindAsync(It.IsAny<FilterDefinition<D>>(),
                It.IsAny<FindOptions<D, D>>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor.Object);
            Mock<DeleteResult> deleteMock = new Mock<DeleteResult>();
            deleteMock.Setup(d => d.DeletedCount).Returns(1);
            mockCollection.Setup(co => co.DeleteOneAsync(It.IsAny<FilterDefinition<D>>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(deleteMock.Object);
            Mock<ReplaceOneResult> updateMock = new Mock<ReplaceOneResult>();
            updateMock.Setup(u => u.ModifiedCount).Returns(1);
            mockCollection.Setup(co => co.ReplaceOneAsync(It.IsAny<FilterDefinition<D>>(), It.IsAny<D>(),
                It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(updateMock.Object);
            Mock<UpdateResult> updateOneMock = new Mock<UpdateResult>();
            updateOneMock.Setup(u => u.ModifiedCount).Returns(1);
            mockCollection.Setup(co => co.UpdateOneAsync(
                It.IsAny<FilterDefinition<D>>(),
                It.IsAny<UpdateDefinition<D>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(updateOneMock.Object);

            this._mongoDatabaseMock.Setup(md => md.GetCollection<D>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                .Returns(mockCollection.Object);
        }

        private static IConfigConexao GetConfig(Mock<IMongoDatabase> mongoDatabaseMock)
        {
            Mock<IConfigConexao> mockConn = new Mock<IConfigConexao>();
            Mock<IMongoClient> mockClient = new Mock<IMongoClient>();
            mockConn.Setup(c => c.MongoDatabase).Returns(mongoDatabaseMock.Object);

            return mockConn.Object;
        }
    }
}
