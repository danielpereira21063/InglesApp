using InglesApp.Data.Context;
using InglesApp.Domain.Entities;
using InglesApp.Domain.Interfaces;

namespace InglesApp.Data.Repositories
{
    public class VocabularioRepository : IVocabularioRepository
    {
        private readonly InglesAppContext _context;

        public VocabularioRepository(InglesAppContext context)
        {
            _context = context;
        }

        public Vocabulario Adicionar(Vocabulario vocabulario)
        {
            _context.Vocabularios.Add(vocabulario);
            return vocabulario;
        }

        public Vocabulario Atualizar(Vocabulario vocabulario)
        {
            _context.Vocabularios.Update(vocabulario);
            return vocabulario;
        }

        public Vocabulario Obter(int id)
        {
            return _context.Vocabularios.FirstOrDefault(v => v.Id == id);
        }

        public ICollection<Vocabulario> ObterPesquisa(string busca, int userId)
        {
            return _context.Vocabularios
                .Where(v => (v.EmIngles.StartsWith(busca) || v.Traducao.Contains(busca) )&& v.UserId == userId)
                .ToList();
        }
    }
}
