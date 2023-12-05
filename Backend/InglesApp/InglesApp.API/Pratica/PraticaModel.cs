using InglesApp.Application.Dto;

namespace InglesApp.API.Pratica
{
    public class PraticaModel
    {
        public PraticaModel(VocabularioDto vocabulario, string[] opcoes, bool praticaDeTraducao)
        {
            VocabularioId = vocabulario.Id;
            PraticaDeTraducao = praticaDeTraducao;

            if (praticaDeTraducao)
            {
                Questao = $"Qual a tradução de { '"' + vocabulario.EmIngles + '"'}?";
                RespostaCorreta = vocabulario.Traducao;
            }
            else
            {
                Questao = $"Como se diz {'"' + vocabulario.Traducao + '"'}?";
                RespostaCorreta = vocabulario.EmIngles;
            }

            Opcoes = opcoes;
        }

        public VocabularioDto Vocabulario { get; set; }
        public int VocabularioId { get; set; }
        public string Questao { get; set; }
        public string RespostaCorreta { get; set; }
        public bool PraticaDeTraducao { get; set; }
        public string[] Opcoes { get; set; }
    }
}
