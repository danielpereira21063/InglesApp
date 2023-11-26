﻿using InglesApp.Application.Dto;
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
                    EmIngles = voc.EmIngles,
                    Explicacao = voc.Explicacao,
                    TipoVocabulario = voc.TipoVocabulario.ToString(),
                    Traducao = voc.Traducao
                })
                .ToList();
        }

        public VocabularioDto Salvar(VocabularioDto dto, int userId)
        {
            Enum.TryParse<TipoVocabulario>(dto.TipoVocabulario, out TipoVocabulario tipo);

            var voc = new Vocabulario(userId, tipo, dto.EmIngles, dto.Traducao, dto.Explicacao);

            return new VocabularioDto()
            {
                EmIngles = voc.EmIngles,
                Explicacao = voc.Explicacao,
                TipoVocabulario = voc.TipoVocabulario.ToString(),
                Traducao = voc.Traducao
            };
        }
    }
}