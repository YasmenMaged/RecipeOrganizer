namespace RO.Repo;

public interface IRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        string Insert(T entity);
        string Update(T entity, Guid id);
        void Delete(T entity);
        string Remove(T entity);
        void SaveChanges();
    }
}