using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.Fila.Publicador;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;
using System.Security.Claims;

namespace Simulado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly IServiceTeste _serviceTeste;
        private readonly IPublicadorBase _publicador;
        public TesteController(IServiceTeste serviceTeste, IPublicadorBase publicador)
        {
            this._serviceTeste = serviceTeste;
            this._publicador = publicador;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            try
            {
                Teste? teste = await this._serviceTeste.GetById(id);
                if (teste == null) return NotFound();
                return Ok(teste);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("filtro")]
        public async Task<IActionResult> Get([FromQuery] TesteFiltro filtro)
        {
            try
            {
                IEnumerable<Teste> teste = await this._serviceTeste.GetManyByFilter(filtro);
                return Ok(teste);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TesteDTO testeDTO)
        {
            string userEmail = this.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            return Ok(await this._serviceTeste.Add(testeDTO, userEmail));
        }

        [Authorize]
        [HttpPost("responder")]
        public async Task<IActionResult> Responder([FromBody] RelatorioSimuladoDTO relatorio)
        {
            EventoDTO<RelatorioSimuladoDTO> evento = new EventoDTO<RelatorioSimuladoDTO>()
            {
                Relatorio = relatorio,
                EmailUser = this.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value
            };
            this._publicador.PublicaMensagem("simulado", "responderQuestao", this._publicador.ConverteMensagem(evento));
            return Ok(evento);
        }
    }
}
