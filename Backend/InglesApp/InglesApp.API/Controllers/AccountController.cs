using InglesApp.Application.Dto;
using InglesApp.Application.Services.Interfaces;
using InglesApp.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InglesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }


        [HttpGet("UsuarioPadrao")]
        public IActionResult UsuarioPadrao([FromQuery] string senha)
        {
            if (_accountService.ObterTodosUsuarios().Count > 0)
            {
                return Ok("Usuário padrão já cadastrado");
            }

            var user = new User()
            {
                Email = "danielsanches6301@gmail.com",
                UserName = "daniel",
            };

            user = _accountService.CriarContaAsync(user, senha).Result;

            return Ok("Usuário padrão criado com sucesso");


        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            try
            {
                var user = await _accountService.ObterUsuarioAsync(login.Login);

                if (user == null) return Unauthorized("Usuário ou senha incorretos");

                var result = await _accountService.ValidarSenhaAsync(login.Login, login.Senha);

                if (!result.Succeeded) return Unauthorized("Usuário ou senha incorretos");

                return Ok(new
                {
                    Token = await _tokenService.GerarTokenAsync(user.UserName),
                    UserName = user.UserName,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
