namespace InglesApp.Application.Dto
{
    public class PraticaDto
    {
        public int Id { get; set; }
        public int VocabularioId { get; set; }
        public int UserId { get; set; }
        public string Resposta { get; set; }
        public bool Acertou { get; set; }

        //indica em porcentagem o quanto a respota do usuário se parece com a resposta correta
        public double SimilaridadeDeAcerto {  get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
