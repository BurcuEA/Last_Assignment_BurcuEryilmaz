using Last_Assignment.Core.UnitOfWork;

namespace Last_Assignment.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        // SaveChange metodunu çağırabilmek için dbcontext e ihtiyaç var...
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext appDbContext)
        {           
            _context = appDbContext;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
