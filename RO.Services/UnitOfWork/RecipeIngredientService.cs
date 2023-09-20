namespace RO.Services;

public class RecipeIngredientService : IRecipeIngredientService
{
    private readonly IRecipeIngredientRepository _recipeIngredientRepository;

    public RecipeIngredientService(IRecipeIngredientRepository recipeIngredientRepository)
    {
        _recipeIngredientRepository = recipeIngredientRepository;
    }

    public IEnumerable<Recipe_Ingredient> GetAll() => _recipeIngredientRepository.GetAll();
    public Recipe_Ingredient Get(Guid id) => _recipeIngredientRepository.Get(id);
    public string Insert(RecipeIngredientRequest entity) => _recipeIngredientRepository.Insert(entity);
    public string Update(Recipe_Ingredient entity, Guid id)
    {
        id = entity.Id;
        Recipe_Ingredient recipeIngredientFromDb = _recipeIngredientRepository.Get(id);
        recipeIngredientFromDb.Recipe = entity.Recipe;
        recipeIngredientFromDb.Ingredient = entity.Ingredient;

        return _recipeIngredientRepository.Update(entity, id);
    }

    public string Delete(Guid id)
    {
        Recipe_Ingredient recipeIngredient = _recipeIngredientRepository.Get(id);
        _recipeIngredientRepository.Remove(recipeIngredient);
        _recipeIngredientRepository.SaveChanges();

        return "Removed!!";
    }

}