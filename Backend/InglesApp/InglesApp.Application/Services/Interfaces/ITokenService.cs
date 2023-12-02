namespace InglesApp.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GerarTokenAsync(string userName);
    }
}
