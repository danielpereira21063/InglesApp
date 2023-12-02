using InglesApp.Application.Dto;
using InglesApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InglesApp.Application.Services.Interfaces
{
    public interface IVocabularioService
    {
        VocabularioDto Salvar(VocabularioDto dto, int userId);
        ICollection<VocabularioDto> ObterPesquisa(string pesquisa, int userId, TipoVocabulario? tipo);
        VocabularioDto Obter(int id);
    }
}
