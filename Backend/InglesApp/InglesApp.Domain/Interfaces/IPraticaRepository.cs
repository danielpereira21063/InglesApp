using InglesApp.Domain.Entities;
using InglesApp.Domain.Enums;
namespace InglesApp.Domain.Interfaces
{
    public interface IPraticaRepository
    {
        ICollection<Pratica> ObterPesquisa(TipoVocabulario? tipo, DateTime de, DateTime ate);
        Pratica Adicionar(Pratica pratica);
    }
}
