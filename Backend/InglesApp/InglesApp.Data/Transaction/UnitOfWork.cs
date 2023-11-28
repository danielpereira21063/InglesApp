using InglesApp.Data.Context;

namespace InglesApp.Data.Transaction
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InglesAppContext _context;

        public UnitOfWork(InglesAppContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public async Task<bool> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
