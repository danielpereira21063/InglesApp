using InglesApp.Application.Dto;
using InglesApp.Application.Services.Interfaces;
using InglesApp.Domain.Entities;
using InglesApp.Domain.Enums;
using InglesApp.Domain.Interfaces;

namespace InglesApp.Application.Services
{
    public class VocabularioService : IVocabularioService
    {
        private readonly IVocabularioRepository _vocabularioRepository;

        public VocabularioService(IVocabularioRepository vocabularioRepository)
        {
            _vocabularioRepository = vocabularioRepository;
        }

        public VocabularioDto Obter(int id)
        {
            var voc = _vocabularioRepository.Obter(id);

            return new VocabularioDto()
            {
                EmIngles = voc.EmIngles,
                Explicacao = voc.Explicacao,
                TipoVocabulario = voc.TipoVocabulario.ToString(),
                Traducao = voc.Traducao
            };
        }

        public ICollection<VocabularioDto> ObterPesquisa(string pesquisa, int userId)
        {
            return _vocabularioRepository.ObterPesquisa(pesquisa, userId)
                .Select(voc => new VocabularioDto()
                {
                    Id = voc.Id,
                    EmIngles = voc.EmIngles,
                    Explicacao = voc.Explicacao,
                    TipoVocabulario = voc.TipoVocabulario.ToString(),
                    Traducao = voc.Traducao
                })
                .ToList();
        }

        public VocabularioDto Salvar(VocabularioDto dto, int userId)
        {
            var tipoSalvar = char.ToUpper(dto.TipoVocabulario[0]) + dto.TipoVocabulario.Substring(1);

            Enum.TryParse<TipoVocabulario>(tipoSalvar, out TipoVocabulario tipo);

            var voc = new Vocabulario(userId, tipo, dto.EmIngles, dto.Traducao, dto.Explicacao);

            _vocabularioRepository.Adicionar(voc);

            return new VocabularioDto()
            {
                Id = voc.Id,
                EmIngles = voc.EmIngles,
                Explicacao = voc.Explicacao,
                TipoVocabulario = voc.TipoVocabulario.ToString(),
                Traducao = voc.Traducao
            };
        }
    }
}
