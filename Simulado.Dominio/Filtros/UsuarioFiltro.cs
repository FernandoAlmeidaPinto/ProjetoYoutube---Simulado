using MongoDB.Driver;

namespace Simulado.Dominio.Filtros
{
    public class UsuarioFiltro : IFiltro<Usuario>
    {
        public string? Email { get; set; }
        public FilterDefinition<Usuario> GetFiltro()
        {
            List<FilterDefinition<Usuario>> listaFiltro = new();

            if(!String.IsNullOrEmpty(Email))
            {
                listaFiltro.Add(Builders<Usuario>.Filter.Eq("email", this.Email));
            }

            if(!listaFiltro.Any())
            {
                return Builders<Usuario>.Filter.Empty;
            }
            return Builders<Usuario>.Filter.And(listaFiltro);
        }
    }
}
