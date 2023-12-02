using System.Security.Claims;

namespace InglesApp.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string ObterNomeDoUsuarioAtual(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int ObterIdDoUsuarioAtual(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
