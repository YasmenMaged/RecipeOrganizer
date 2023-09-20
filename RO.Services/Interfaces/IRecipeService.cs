namespace RO.Services;

public interface IRecipeService
{
    IEnumerable<Recipe> GetAllRecipes();

    Recipe GetRecipeById(Guid id);

    string AddRecipe(Recipe recipe);

    string UpdateRecipe(Recipe recipe, Guid id);

    string RemoveRecipe(Guid id);
}