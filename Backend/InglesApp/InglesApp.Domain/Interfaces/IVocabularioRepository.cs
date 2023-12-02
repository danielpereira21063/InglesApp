using InglesApp.Domain.Entities;
using InglesApp.Domain.Enums;

namespace InglesApp.Domain.Interfaces
{
    public interface IVocabularioRepository
    {
        Vocabulario Obter(int id);
        ICollection<Vocabulario> ObterPesquisa(string busca, int userId, TipoVocabulario? tipo, DateTime de, DateTime ate);
        Vocabulario Adicionar(Vocabulario vocabulario);
        Vocabulario Atualizar(Vocabulario vocabulario);
    }
}
