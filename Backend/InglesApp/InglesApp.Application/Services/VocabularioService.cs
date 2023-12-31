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
                Id = voc.Id,
                UserId = voc.UserId,
                EmIngles = voc.EmIngles,
                Explicacao = voc.Explicacao,
                TipoVocabulario = voc.TipoVocabulario.ToString(),
                Traducao = voc.Traducao
            };
        }

        public ICollection<VocabularioDto> ObterPesquisa(string pesquisa,
                                                         int userId,
                                                         TipoVocabulario? tipo,
                                                         DateTime de,
                                                         DateTime ate,
                                                         bool praticando = false,
                                                         int limite = 1000)
        {
            return _vocabularioRepository.ObterPesquisa(pesquisa, userId, tipo, de, ate, praticando: praticando, limite: limite)
                .Select(voc => new VocabularioDto()
                {
                    Id = voc.Id,
                    UserId = voc.UserId,
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
            if (dto.Id >= 1)
            {
                var antigo = _vocabularioRepository.Obter(dto.Id);

                voc.Id = antigo.Id;
                voc.CreatedAt = antigo.CreatedAt;

                _vocabularioRepository.Atualizar(voc);
            }
            else
            {
                _vocabularioRepository.Adicionar(voc);
            }


            return new VocabularioDto()
            {
                Id = voc.Id,
                UserId = voc.UserId,
                EmIngles = voc.EmIngles,
                Explicacao = voc.Explicacao,
                TipoVocabulario = voc.TipoVocabulario.ToString(),
                Traducao = voc.Traducao
            };
        }
    }
}
