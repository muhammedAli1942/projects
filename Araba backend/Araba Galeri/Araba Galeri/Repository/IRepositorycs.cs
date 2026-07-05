namespace Araba_Galeri.Repository
{
    public interface IRepositorycs<T>where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddSync(T entity);

        Task UpdateSync(T entity);
        Task DeleteSync(int id);
    }
}
