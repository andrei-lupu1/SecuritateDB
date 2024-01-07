namespace Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}