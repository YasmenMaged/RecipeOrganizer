namespace RO.Services;

public class IngredientService : IIngredientService
{
    private  IRepository<Ingredient> _repository { get; set; }
    private readonly IMemoryCache _memoryCache;
    string errorMessage = string.Empty;
    private readonly string cachekey = "IngredientCacheKey";

    public IngredientService(IRepository<Ingredient> repository, IMemoryCache memoryCache)
    {
        _repository = repository;
        _memoryCache = memoryCache;
    }

    public IEnumerable<Ingredient> GetAllIngredients() => _repository.GetAll();

    public Ingredient GetIngredientById(Guid id) => _repository.Get(id);

    public string AddIngredient(Ingredient Ingredient) => _repository.Insert(Ingredient);

    public string UpdateIngredient(Ingredient Ingredient, Guid id)
    {
        id = Ingredient.Id;
        
        return _repository.Update(Ingredient, id);
    }

    public string RemoveIngredient(Guid id)
    {
        Ingredient Ingredient = _repository.Get(id);
        _repository.Remove(Ingredient);
        _repository.SaveChanges();

        return "Removed!!";
    }
}