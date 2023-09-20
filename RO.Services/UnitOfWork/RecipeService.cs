namespace RO.Services;

public class RecipeService : IRecipeService
{
    private IRepository<Recipe> _repository { get; set; }
    public RecipeService(IRepository<Recipe> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Recipe> GetAllRecipes() => _repository.GetAll();

    public Recipe GetRecipeById(Guid id) => _repository.Get(id);

    public string AddRecipe(Recipe recipe) => _repository.Insert(recipe);

    public string UpdateRecipe(Recipe recipe, Guid id)
    {
        id = recipe.Id;
        Recipe recipeFromDb = _repository.Get(id);
        recipeFromDb.Steps = recipe.Steps;
        recipeFromDb.Description = recipe.Description;

        return _repository.Update(recipe, id);
    }

    public string RemoveRecipe(Guid id)
    {

        Recipe recipe = _repository.Get(id);
        _repository.Remove(recipe);
        _repository.SaveChanges();

        return "Removed!!";
    }
}