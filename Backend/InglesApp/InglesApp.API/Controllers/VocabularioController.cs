using InglesApp.API.Extensions;
using InglesApp.Application.Dto;
using InglesApp.Application.Services.Interfaces;
using InglesApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InglesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VocabularioController : ControllerBase
    {
        private readonly IVocabularioService _vocabularioService;
        private int _usuarioId => User.ObterIdDoUsuarioAtual();

        public VocabularioController(IVocabularioService vocabularioService)
        {
            _vocabularioService = vocabularioService;
        }



        [HttpPost]
        public IActionResult Post(VocabularioDto vocabularioDto)
        {
            if (vocabularioDto.Id > 0)
            {
                if (vocabularioDto.UserId != _usuarioId) return BadRequest("Vocabulário não pertence ao usuário logado");
            }

            var voc = _vocabularioService.Salvar(vocabularioDto, _usuarioId);

            return Ok("Salvo com sucesso");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var voc = _vocabularioService.Obter(id);

            if (voc.UserId != _usuarioId)
            {
                return BadRequest("Vocabulário não pertence ao usuário");
            }

            voc.Inativo = true;

            _vocabularioService.Salvar(voc, _usuarioId);

            return Ok("Deletado com sucesso");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var vocabulario = _vocabularioService.Obter(id);

            if (vocabulario == null) return BadRequest("Vocabulário não existe");

            if (vocabulario.UserId != _usuarioId) return BadRequest("Vocabulário não pertence ao usuário logado");

            return Ok(vocabulario);
        }


        [HttpGet("Pesquisar")]
        public IActionResult Pesquisar([FromQuery] string? pesquisa = null, [FromQuery] int tipo = 0, [FromQuery] int periodo = 1)
        {
            var de = DateTime.Now.Date;
            var ate = DateTime.Now;

            switch (periodo) {
                case 1:
                    break;
                case 2:
                    de = DateTime.Now.Date.AddDays(-7);
                    break;
                case 3:
                    de = DateTime.Now.Date.AddDays(-14);
                        break;
                case 4:
                    de = DateTime.Now.Date.AddDays(-30);
                    break;
                case 5:
                    de = DateTime.Now.Date.AddYears(-100);
                    break;
            }

            var vocs = _vocabularioService.ObterPesquisa(pesquisa ?? "", _usuarioId, (TipoVocabulario)tipo, de, ate);

            return Ok(vocs);
        }
    }
}
