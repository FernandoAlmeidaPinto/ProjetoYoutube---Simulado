using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simulado.Dominio;
using Simulado.DTO;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;

namespace Simulado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IServiceUser _serviceUsuario;
        public UserController(IServiceUser serviceUsuario)
        {
            this._serviceUsuario = serviceUsuario;
        }

        [Authorize(Roles = "Professor")]
        [HttpGet]
        public async Task<IActionResult> GetUserByID([FromQuery] string id)
        {
            Usuario? user = await this._serviceUsuario.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDTO usuario)
        {
            if(await this._serviceUsuario.Add(usuario)) return Ok(usuario); 
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            LoginDTOOutput loginOutput = await this._serviceUsuario.Login(login);
            if (loginOutput.Status == "Ok") return Ok(loginOutput);
            return BadRequest(loginOutput.Status);
        }
    }
}
