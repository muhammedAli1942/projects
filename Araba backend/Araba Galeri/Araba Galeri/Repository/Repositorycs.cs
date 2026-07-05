using Araba_Galeri.Models;
using Microsoft.EntityFrameworkCore;

namespace Araba_Galeri.Repository
{
    public class Repositorycs<T> : IRepositorycs<T> where T : class
    {
        private readonly ArabaGaleriContext _context;
        public Repositorycs(ArabaGaleriContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task AddSync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
