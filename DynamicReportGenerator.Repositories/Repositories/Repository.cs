using DynamicReportGenerator.Domain.Interfaces;
using DynamicReportGenerator.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DynamicReportGenerator.Repositories.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}
