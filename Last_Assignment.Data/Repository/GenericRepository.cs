using Last_Assignment.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Last_Assignment.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;  // VT ile ilgili işlemler için 
        private readonly DbSet<TEntity> _dbSet;

        /* API tarafına geçince DI container 'a eklenecek DAHA sonra .StartUP cs de ... configureservices metodunda. 
          Uygulamanın herhangi bir yerinde bir classın ctor unda --- bu arkadaşı (GenericRepository yi görüğünde) ---  AppDbContext bir nesne örneği oluşacak */
        public GenericRepository(AppDbContext context)
        {
            //DbContext ve IdentiyDbContext miras durumu incelemek aklına gelsin..
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            /*Domain Driven... Design kullanılması gereken yerde,çok business kuralı  varsa -GenericRepository ......
             * 
             * Product.GetById(1)
             Product.Nmae = "Kalem"  -- sadece Name ini güncelliyor,
            context.SaveChanges()             
             */

            //Performans açısından GenericRepository kullanmanın nin Dezavantajı ,fakat merkezileştirmek adına kullanıyoruz
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
