using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simulado.Dominio;
using Simulado.Dominio.Filtros;
using Simulado.Service.DTO;
using Simulado.Service.Service.Contratos;
using System.Security.Claims;

namespace Simulado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestaoController : ControllerBase
    {
        private readonly IServiceQuestao _serviceQuestao;
        public QuestaoController(IServiceQuestao serviceQuestao)
        {
            this._serviceQuestao = serviceQuestao;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            try
            {
                Questao? questao = await this._serviceQuestao.GetById(id);
                if(questao == null) return NotFound();
                return Ok(questao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("filtro")]
        public async Task<IActionResult> Get([FromQuery] QuestaoFiltro filtro)
        {
            try
            {
                IEnumerable<Questao> questoes = await this._serviceQuestao.GetManyByFilter(filtro);
                return Ok(questoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QuestaoDTO questao)
        {
            string userEmail = this.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            return Ok(await this._serviceQuestao.Add(questao, userEmail));
        }
    }
}
