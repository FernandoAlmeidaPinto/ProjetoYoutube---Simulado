using AutoMapper;
using DnsClient;
using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.DTO;
using Simulado.Repositorio.Repositorios.Contratos;
using Simulado.Service.ConfigCript;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;
using BC = BCrypt.Net.BCrypt;

namespace Simulado.Service.Service
{
    public class ServiceUser : ServiceBase<UsuarioDTO, Usuario>, IServiceUser
    {
        private readonly ServiceToken _serviceToken;
        private readonly IRepositorioBase<Usuario> _repositorio;
        private readonly IMapper _autoMapper;

        public ServiceUser(IRepositorioBase<Usuario> repositorio, IMapper _autoMapper, ServiceToken serviceToken) : base(repositorio, _autoMapper)
        {
            this._repositorio = repositorio;
            this._autoMapper = _autoMapper;
            this._serviceToken = serviceToken;
            
        }
        public async Task<LoginDTOOutput> Login(LoginDTO login)
        {
            Usuario? user = (await this._repositorio.GetManyByFilter(new UsuarioFiltro() { Email = login.Email })).FirstOrDefault();
            if (user == null) { return new LoginDTOOutput() { Status = "Usuario nao existe" }; }
            if(!this.VerificaSenha(login.Senha, user.Senha)) { return new LoginDTOOutput() { Status = "Senha incorreta" }; }
            string token = this._serviceToken.GenerateToken(user);
            return new LoginDTOOutput()
            {
                Nome = user.Nome,
                Email = login.Email,
                Token = token,
                Status = "Ok"
            };
        }

        public async override Task<bool> Add(UsuarioDTO userDTO)
        {
            Usuario userDomain = this._autoMapper.Map<Usuario>(userDTO);
            userDomain.Senha = this.GeraSenhaHash(userDomain.Senha);
            return await this._repositorio.Add(userDomain);
        }

        private string GeraSenhaHash(string senha)
        {
            return BC.HashPassword(senha, 12);
        }

        private bool VerificaSenha(string senha, string hash)
        {
            return BC.Verify(senha, hash);
        }
    }
}
