using Microsoft.EntityFrameworkCore;

namespace RO.Services;

public class RecipeService : IRecipeService
{
    private IRepository<Recipe> _repository { get; set; }
    private AppDbContext _recipeContext { get; set; }

    public RecipeService(IRepository<Recipe> repository, AppDbContext recipeContext)
    {
        _repository = repository;
        _recipeContext = recipeContext;
    }

    public IEnumerable<Recipe> GetAllRecipes() => _repository.GetAll();

    public string AddRecipe(Recipe recipe) => _repository.Insert(recipe);

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
    public string RemoveRecipe(Guid id)
    {

        Recipe recipe = _repository.Get(id);
        _repository.Remove(recipe);
        _repository.SaveChanges();

        return "Removed!!";
    }
}