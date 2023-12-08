using InglesApp.Application.Dto;
using InglesApp.Application.Services.Interfaces;
using InglesApp.Domain.Entities;
using InglesApp.Domain.Enums;
using InglesApp.Domain.Interfaces;
using System.Globalization;
using System.Text;

namespace InglesApp.Application.Services
{
    public class PraticaService : IPraticaService
    {
        private readonly IPraticaRepository _praticaRepository;
        private readonly IVocabularioRepository _vocabularioRepository;

        public PraticaService(IPraticaRepository praticaRepository, IVocabularioRepository vocabularioRepository)
        {
            _praticaRepository = praticaRepository;
            _vocabularioRepository = vocabularioRepository;
        }


        public PraticaDto Adicionar(PraticaDto pratica)
        {
            var similiaridade = VerificarSimilaridade(pratica);

            //se acertou 95% da palavra/frase significa que acertou
            var acertou = similiaridade > 90.0;

            var novaPratica = new Pratica(pratica.VocabularioId, pratica.UserId, pratica.Resposta, acertou, pratica.PraticaDeTraducao);


            _praticaRepository.Adicionar(novaPratica);

            return new PraticaDto()
            {
                Id = novaPratica.Id,
                VocabularioId = novaPratica.VocabularioId,
                UserId = novaPratica.UserId,
                Acertou = novaPratica.Acertou,
                PraticaDeTraducao = novaPratica.PraticaDeTraducao,
                SimilaridadeDeAcerto = similiaridade,
                Resposta = novaPratica.Resposta,
                CreatedAt = novaPratica.CreatedAt
            };
        }

        public ICollection<PraticaDto> ObterPesquisa(TipoVocabulario? tipo, DateTime de, DateTime ate)
        {
            return _praticaRepository
                .ObterPesquisa(tipo, de, ate)
                .Select(p => new PraticaDto()
                {
                    Id = p.Id,
                    VocabularioId = p.VocabularioId,
                    UserId = p.UserId,
                    Acertou = p.Acertou,
                    PraticaDeTraducao = p.PraticaDeTraducao,
                    Resposta = p.Resposta,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }

        public double VerificarSimilaridade(PraticaDto pratica)
        {
            var vocabulario = _vocabularioRepository.Obter(pratica.VocabularioId);

            var resposta = RemoverAcentosEEspacos(vocabulario.EmIngles).ToLower();
            var respostaUsuario = RemoverAcentosEEspacos(pratica.Resposta).ToLower();

            if (pratica.PraticaDeTraducao)
            {
                resposta = RemoverAcentosEEspacos(vocabulario.Traducao).ToLower();
            }

            // Calcula a distância de Levenshtein
            int distancia = CalcularDistanciaLevenshtein(resposta, respostaUsuario);

            // Calcula a porcentagem de similaridade normalizando pela maior string
            double similaridade = 100.0 - ((double)distancia / Math.Max(resposta.Length, respostaUsuario.Length)) * 100.0;

            // Retorna verdadeiro se a similaridade for pelo menos 95%
            return similaridade;
        }

        private int CalcularDistanciaLevenshtein(string str1, string str2)
        {
            int len1 = str1.Length;
            int len2 = str2.Length;
            int[,] matriz = new int[len1 + 1, len2 + 1];

            for (int i = 0; i <= len1; i++)
                matriz[i, 0] = i;

            for (int j = 0; j <= len2; j++)
                matriz[0, j] = j;

            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    int custo = (str2[j - 1] == str1[i - 1]) ? 0 : 1;

                    matriz[i, j] = Math.Min(
                        Math.Min(matriz[i - 1, j] + 1, matriz[i, j - 1] + 1),
                        matriz[i - 1, j - 1] + custo
                    );
                }
            }

            return matriz[len1, len2];
        }

        private string RemoverAcentosEEspacos(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark && !char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}