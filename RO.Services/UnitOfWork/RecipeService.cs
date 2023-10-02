namespace RO.Services;

public class RecipeService : IRecipeService
{
    private IRepository<Recipe> _repository { get; set; }
    private AppDbContext _recipeContext { get; set; }

    private DbSet<Recipe> entities;
    private readonly IMemoryCache _memoryCache;
    private readonly string cachekey = "RecipeCacheKey";

    public RecipeService(IRepository<Recipe> repository, AppDbContext recipeContext, IMemoryCache memoryCache)
    {
        _repository = repository;
        _memoryCache = memoryCache;
        _recipeContext = recipeContext;
        entities = _recipeContext.Recipes;
    }

    public IEnumerable<Recipe> GetAllRecipes() => _repository.GetAll();

    public string AddRecipe(Recipe recipe)
    {
        Recipe Recipe = new Recipe()
        {

            Name = recipe.Name,
            Category = recipe.Category
        };

        return _repository.Insert(Recipe);
    }

    public string UpdateRecipe(Recipe recipe, Guid id)
    {
        id = recipe.Id;
        Recipe recipeFromDb = _repository.Get(id);
        recipeFromDb.Steps = recipe.Steps;
        recipeFromDb.Description = recipe.Description;

        return _repository.Update(recipe, id);
    }

    public RecipeWithIngredientsVM GetRecipeWithIngredient(Guid id)
    {
        var Recipe = _recipeContext.Recipes.Where(n => n.Id == id).Select(n => new RecipeWithIngredientsVM()
        {
            Name = n.Name,
            IngredientName = n.Recipe_Ingredient.Select(n => n.Ingredient.Name).ToList()
        }).FirstOrDefault();

        return Recipe;
    }
    public string GetRecipeByListOfIngredients(List<string> Ingredients)
    {
        foreach (Recipe Recipe in _recipeContext.Recipes.ToList())
        {

            var recipeFromDb = _recipeContext.Recipes.Where(n => n.Id == Recipe.Id).Select(n => new RecipeWithIngredientsVM()
            {
                Name = n.Name,
                IngredientName = n.Recipe_Ingredient.Select(n => n.Ingredient.Name).ToList()
            }).FirstOrDefault();
            if (recipeFromDb.IngredientName.Count() != Ingredients.Count())
            {
                return ("Please enter the correct Ingredients.");
            }
            recipeFromDb.IngredientName.Sort();
            Ingredients.Sort();

            bool exist = true;
            for (int i = 0; i < recipeFromDb.IngredientName.Count(); i++)
            {
                if (recipeFromDb.IngredientName[i] != Ingredients[i])
                {
                    exist = false;
                    break;

                }
            }
            if (exist == true)
            {
                return (Recipe.Name);
            }

        }

        return ("No matching recipes found.");
    }

    public List<Recipe> GetRecipesByCategory(string category)
    {
        return _repository.GetAllWithFilter(f => f.Category.Contains(category));
    }

    public RecipeWithFeedBacks GetRecipeWithFeedBacks(Guid id)
    {

        return _memoryCache.GetOrCreate($"id:{id}", cacheEntry =>
        {
            var Recipe = entities.Where(n => n.Id == id).Select(n => new RecipeWithFeedBacks()
            {
                Name = n.Name,
                Rates = n.FeedBacks.Select(n => n.Rate).ToList(),
                Reviews = n.FeedBacks.Select(n => n.Review).ToList()
            }).FirstOrDefault();
            var cacheOptions = new MemoryCacheEntryOptions()
           .SetSize(1) // maximum cache size in number of entries
           .SetSlidingExpiration(TimeSpan.FromMinutes(5)) // cache expiration time after last access
           .SetAbsoluteExpiration(TimeSpan.FromMinutes(30)); // cache expiration time after creation

            _memoryCache.Set(cachekey, Recipe, cacheOptions);

            return Recipe;

        });
    }

    public string RemoveRecipe(Guid id)
    {

        Recipe recipe = _repository.Get(id);
        _repository.Remove(recipe);
        _repository.SaveChanges();

        return "Removed!!";
    }
}