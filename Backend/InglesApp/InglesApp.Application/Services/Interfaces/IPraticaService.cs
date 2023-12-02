using InglesApp.Application.Dto;
using InglesApp.Domain.Enums;

namespace InglesApp.Application.Services.Interfaces
{
    public interface IPraticaService
    {
        ICollection<PraticaDto> ObterPesquisa(TipoVocabulario? tipo, DateTime de, DateTime ate);
        PraticaDto Adicionar(PraticaDto pratica);
    }
}
