using InglesApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace InglesApp.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User> CriarContaAsync(User userDto, string senha);
        Task<User> ObterUsuarioAsync(string nomeUsuario);
        ICollection<User> ObterTodosUsuarios();
        Task<SignInResult> ValidarSenhaAsync(string nomeUsuario, string senha);
        Task<User> AlterarDadosDaContaAsync(User user);
        Task<bool> UsuarioExite(string nomeUsuario);
    }
}
