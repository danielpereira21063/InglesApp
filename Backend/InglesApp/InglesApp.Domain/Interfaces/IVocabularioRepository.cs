using InglesApp.Domain.Entities;

namespace InglesApp.Domain.Interfaces
{
    public interface IVocabularioRepository
    {
        Vocabulario Obter(int id);
        ICollection<Vocabulario> ObterPesquisa(string busca, int userId);
        Vocabulario Adicionar(Vocabulario vocabulario);
        Vocabulario Atualizar(Vocabulario vocabulario);
    }
}
