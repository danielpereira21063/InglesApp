using InglesApp.Domain.Enums;

namespace InglesApp.Domain.Entities
{
    public sealed class Vocabulario : AbstractEntity
    {
        public Vocabulario()
        {

        }

        public Vocabulario(int userId, TipoVocabulario tipoVocabulario, string emIngles, string traducao, string explicacao)
        {
            if (emIngles.Split(" ").Length == 1 && tipoVocabulario == TipoVocabulario.Frase)
            {
                tipoVocabulario = TipoVocabulario.Palavra;
            }

            UserId = userId;

            TipoVocabulario = tipoVocabulario;
            EmIngles = emIngles;
            Traducao = traducao;
            Explicacao = explicacao;

            Validar();
        }

        public int UserId { get; set; }
        public TipoVocabulario TipoVocabulario { get; set; }
        public string EmIngles { get; set; }
        public string Traducao { get; set; }
        public string Explicacao { get; set; }
        public bool Inativo { get; set; }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(EmIngles))
            {
                throw new ArgumentException("A expressão em inglês é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(Traducao))
            {
                throw new ArgumentException("A tradução é obrigatória.");
            }
        }
    }
}
