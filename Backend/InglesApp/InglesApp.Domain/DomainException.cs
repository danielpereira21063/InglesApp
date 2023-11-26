namespace InglesApp.Domain
{
    public class DomainException : Exception
    {
        public ICollection<string> MsgErros { get; set; }

        public DomainException(string erro) : base(erro)
        {

        }

        public DomainException(ICollection<string> msgErros)
        {
            MsgErros = msgErros;
        }
    }
}
