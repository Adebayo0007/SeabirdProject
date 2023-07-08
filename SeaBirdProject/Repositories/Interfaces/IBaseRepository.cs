namespace SeaBirdProject.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T user);
        T GetById(string userId);
        T GetByEmail(string userEmail);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllNonActiveAsync();
        T Update(T user);
        Task Delete(T user);
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
