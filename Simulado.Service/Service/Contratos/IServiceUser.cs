using Simulado.Dominio;
using Simulado.DTO;
using Simulado.Service.DTO;

namespace Simulado.Service.Service.Contratos
{
    public interface IServiceUser : IServiceBase<UsuarioDTO, Usuario>
    {
        Task<LoginDTOOutput> Login(LoginDTO login);
    }
}
