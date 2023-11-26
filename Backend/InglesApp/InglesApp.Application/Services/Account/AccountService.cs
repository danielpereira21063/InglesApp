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

        public async Task<User> CriarContaAsync(User userDto, string senha)
        {
            try
            {
                var user = userDto;

                var result = await _userManager.CreateAsync(user, senha);

                if (!result.Succeeded)
                {
                    return null;
                }

                return user;
            }
            catch (Exception)
            {
                throw new Exception($"Erro ao tentar criar Usuário.");
            }

        }

        public async Task<User> ObterUsuarioAsync(string nomeUsuario)
        {
            return await _userManager.FindByNameAsync(nomeUsuario);
        }

        public async Task<bool> UsuarioExite(string nomeUsuario)
        {
            return await _userManager.FindByNameAsync(nomeUsuario) != null;
        }

        public async Task<SignInResult> ValidarSenhaAsync(string nomeUsuario, string senha)
        {
            var user = _userManager.Users.FirstOrDefault(user => user.UserName == nomeUsuario);

            if (user == null) return null;

            return await _signInManager.CheckPasswordSignInAsync(user, senha, true);
        }
    }
}
