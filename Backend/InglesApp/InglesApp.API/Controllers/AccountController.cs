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

            var userDto = new UserDto()
            {
                Nome = "Daniel",
                Usuario = "daniel",
            };

            var user = _accountService.CriarContaAsync(userDto, senha).Result;

            return Ok("Usuário padrão criado com sucesso");


        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            try
            {
                var user = _accountService.ObterUsuarioAsync(login.Login);

                if (user == null) return Unauthorized("Usuário ou senha incorretos");

                var result = await _accountService.ValidarSenhaAsync(login.Login, login.Senha);

                if (!result.Succeeded) return Unauthorized("Usuário ou senha incorretos");

                return Ok(new UserDto
                {
                    Nome = user.Nome,
                    Usuario = user.UserName,
                    Token = await _tokenService.GerarTokenAsync(user.UserName),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(UserDto model)
        {
            try
            {
                var user = _accountService.ObterUsuarioAsync(model.Usuario);

                if (user != null) return BadRequest("Usuário já cadastrado");

                user = await _accountService.CriarContaAsync(model, model.Senha);

                if(user == null) return BadRequest("Erro ao criar usuário");

                return Ok(new UserDto
                {
                    Nome = user.Nome,
                    Usuario = user.UserName,
                    Token = await _tokenService.GerarTokenAsync(user.UserName),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
