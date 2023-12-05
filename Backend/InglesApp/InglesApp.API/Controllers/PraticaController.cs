using InglesApp.API.Extensions;
using InglesApp.Application.Dto;
using InglesApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InglesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PraticaController : ControllerBase
    {
        private readonly IPraticaService _praticaService;
        private int _usuarioId => User.ObterIdDoUsuarioAtual();

        public PraticaController(IPraticaService praticaService)
        {
            _praticaService = praticaService;
        }


        //validar no post se é prática de tradução para validar a resposta correta
        [HttpPost]
        public IActionResult Post(PraticaDto pratica)
        {
            pratica.UserId = _usuarioId;

            var praticaDto = _praticaService.Adicionar(pratica);

            return Ok(praticaDto);
        }
    }
}
