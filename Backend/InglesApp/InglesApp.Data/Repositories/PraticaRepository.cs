using InglesApp.Data.Context;
using InglesApp.Domain.Entities;
using InglesApp.Domain.Enums;
using InglesApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InglesApp.Data.Repositories
{
    public class PraticaRepository : IPraticaRepository
    {
        private readonly InglesAppContext _context;

        public PraticaRepository(InglesAppContext context)
        {
            _context = context;
        }

        public Pratica Adicionar(Pratica pratica)
        {
            _context.Add(pratica);
            return pratica;
        }

        public ICollection<Pratica> ObterPesquisa(TipoVocabulario? tipo, DateTime de, DateTime ate)
        {
            var query = _context.Praticas
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Where(p => p.CreatedAt >= de && p.CreatedAt <= ate);

            if (tipo > 0)
            {
                query = query
                    .Include(v => v.Vocabulario)
                    .Where(v => v.Vocabulario.TipoVocabulario == tipo);
            }

            return query
                .ToList();
        }
    }
}
