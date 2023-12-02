using InglesApp.Application.Dto;
using InglesApp.Application.Services.Interfaces;
using InglesApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace InglesApp.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public Task<User> AlterarDadosDaContaAsync(User user)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> ObterTodosUsuarios()
        {
            return _userManager.Users.ToList();
        }

        public async Task<User> CriarContaAsync(UserDto userDto, string senha)
        {
            try
            {
                var result = await _userManager.CreateAsync(new User()
                {
                    Nome = userDto.Nome,
                    UserName = userDto.Usuario
                }, senha);

                if (!result.Succeeded)
                {
                    return null;
                }

                return ObterUsuarioAsync(userDto.Usuario);
            }
            catch (Exception)
            {
                throw new Exception($"Erro ao tentar criar Usuário.");
            }

        }

        public User ObterUsuarioAsync(string nomeUsuarioEmail)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName.ToLower() == nomeUsuarioEmail.ToLower() || x.Email.ToLower() == nomeUsuarioEmail.ToLower());
            return user;
        }

        public async Task<bool> UsuarioExite(string nomeUsuario)
        {
            return await _userManager.FindByNameAsync(nomeUsuario) != null;
        }

        public async Task<SignInResult> ValidarSenhaAsync(string nomeUsuarioEmail, string senha)
        {
            var user = _userManager.Users.FirstOrDefault(user => user.UserName == nomeUsuarioEmail || user.Email == nomeUsuarioEmail);

            if (user == null) return null;

            return await _signInManager.CheckPasswordSignInAsync(user, senha, true);
        }
    }
}
