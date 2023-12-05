using InglesApp.Application.Dto;
using InglesApp.Domain.Enums;

namespace InglesApp.Application.Services.Interfaces
{
    public interface IVocabularioService
    {
        VocabularioDto Salvar(VocabularioDto dto, int userId);
        ICollection<VocabularioDto> ObterPesquisa(string pesquisa,
                                                         int userId,
                                                         TipoVocabulario? tipo,
                                                         DateTime de,
                                                         DateTime ate,
                                                         bool praticando,
                                                         int limite = 5000);
        VocabularioDto Obter(int id);
    }
}
