namespace InglesApp.Domain.Entities
{
    public class Pratica : AbstractEntity
    {
        public Pratica()
        {
                
        }

        public Pratica(int vocabularioId, int userId, string resposta, bool acertou, bool praticaDeTraducao)
        {
            VocabularioId = vocabularioId;
            UserId = userId;
            PraticaDeTraducao = praticaDeTraducao;
            Resposta = resposta;
            Acertou = acertou;
        }

        public int Id { get; set; }
        public Vocabulario Vocabulario { get; set; }
        public int VocabularioId { get; set; }
        public bool PraticaDeTraducao { get; set; }
        public int UserId { get; set; }
        public string Resposta { get; set; }
        public bool Acertou { get; set; }
    }
}
