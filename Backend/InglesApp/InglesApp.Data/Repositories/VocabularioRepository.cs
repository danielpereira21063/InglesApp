using InglesApp.Data.Context;
using InglesApp.Domain.Entities;
using InglesApp.Domain.Enums;
using InglesApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return _context.Vocabularios
                .Where(v => v.Id == id && !v.Inativo)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public ICollection<Vocabulario> ObterPesquisa(string busca, int userId, TipoVocabulario? tipo, DateTime de, DateTime ate, int limite = 5000, bool praticando = false)
        {

            var query = _context.Vocabularios
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Where(v => (v.EmIngles.StartsWith(busca ?? "") || v.Traducao.Contains(busca ?? ""))
                    && v.UserId == userId && !v.Inativo
                    && v.CreatedAt >= de && v.CreatedAt <= ate);

            if (tipo > 0)
            {
                query = query.Where(v => v.TipoVocabulario == tipo);
            }

            if (praticando)
            {
                query = query
                    .OrderBy(x => Guid.NewGuid());
            }

            return query
                .Take(limite)
                .ToList();
        }
    }
}
