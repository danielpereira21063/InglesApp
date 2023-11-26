using InglesApp.Application.Dto;
using InglesApp.Application.Services.Interfaces;
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

        public VocabularioController(IVocabularioService vocabularioService)
        {
            _vocabularioService = vocabularioService;
        }


        [HttpPost]
        public IActionResult Post(VocabularioDto vocabularioDto)
        {
            var voc = _vocabularioService.Salvar(vocabularioDto, 1);

            return Ok("Salvo com sucesso");
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? pesquisa)
        {
            var vocs = _vocabularioService.ObterPesquisa(pesquisa, 1);

            return Ok(vocs);
        }
    }
}
