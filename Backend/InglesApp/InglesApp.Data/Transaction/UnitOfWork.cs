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
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
