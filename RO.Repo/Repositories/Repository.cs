namespace RO.Repo;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> entities;

    public Repository(AppDbContext context)
    {
        _context = context;
        entities = context.Set<T>();
    }
    public IEnumerable<T> GetAll()
    {
        return entities.AsEnumerable();
    }

    public T Get(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));

        return entities.SingleOrDefault(e => e.Id == id);
    }

    public string Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        entity.Id = Guid.NewGuid();
        entities.Add(entity);
        _context.SaveChanges();

        return $"{entity} added successfully !!!";
    }

    public string Update(T entity, Guid id)
    {  
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        T existingData = entities.FirstOrDefault(r => r.Name == entity.Name && r.Id!=id);

       if(existingData is not null)
       {
            return "already exists, You have to enter another name";
       }

        T retrivedData = entities.FirstOrDefault(r => r.Id == id);
        retrivedData.Name = entity.Name; 

        _context.SaveChanges();
        return "Updated successfully";
    }

    public void Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        entities.Remove(entity);
        _context.SaveChanges();
    }

    public string Remove(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        entities.Remove(entity);
        return "Recipe removed successfully !!!";
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}