﻿namespace InglesApp.Application.Dto
{
    public class VocabularioDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TipoVocabulario { get; set; }
        public string EmIngles { get; set; }
        public string Traducao { get; set; }
        public string Explicacao { get; set; }
        public bool Inativo { get; set; }
    }
}
