using InglesApp.API.Extensions;
using InglesApp.API.Pratica;
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
        public IActionResult Pesquisar([FromQuery] string? pesquisa = null,
                                       [FromQuery] int tipo = 0,
                                       [FromQuery] int periodo = 1,
                                       [FromQuery] bool praticando = false)
        {
            try
            {
                var de = DateTime.Now.Date;
                var ate = DateTime.Now;

                switch (periodo)
                {
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

                var vocs = _vocabularioService.ObterPesquisa(pesquisa ?? "", _usuarioId, (TipoVocabulario)tipo, de, ate, praticando: praticando);

                Random random = new Random();
                if (praticando)
                {
                    vocs = vocs.OrderBy(v => Guid.NewGuid()).ToList();

                    var retorno = new List<PraticaModel>();
                    foreach (var vocabulario in vocs)
                    {
                        var praticaDeTraducao = random.Next(1, 101) % 2 == 0;

                        var numRandom = random.Next(1, 101);

                        List<string> opcoes = new List<string>();

                        //var vocsOpcoes = _vocabularioService.ObterPesquisa("", _usuarioId, (TipoVocabulario)tipo, de, ate, praticando: true, 5000);

                        if (numRandom < 51)
                        {
                            if (praticaDeTraducao)
                            {
                                opcoes = vocs
                                    .OrderBy(x => Guid.NewGuid())
                                    .Where(x => x.Traducao != vocabulario.Traducao)
                                    .Select(v => v.Traducao)
                                    .Distinct()
                                    .Take(3)
                                    .ToList();

                                opcoes.Insert(opcoes.Count - 1, vocabulario.Traducao);
                            }
                            else
                            {
                                opcoes = vocs
                                    .OrderBy(x => Guid.NewGuid())
                                    .Where(x => x.EmIngles != vocabulario.EmIngles)
                                    .Select(v => v.EmIngles)
                                    .Distinct()
                                    .Take(3)
                                    .ToList();

                                opcoes.Insert(opcoes.Count - 1, vocabulario.EmIngles);

                            }

                        }

                        var model = new PraticaModel(vocabulario, opcoes.OrderBy(x => Guid.NewGuid()).ToArray(), praticaDeTraducao);

                        retorno.Add(model);
                    }

                    return Ok(retorno);
                }

                return Ok(vocs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
