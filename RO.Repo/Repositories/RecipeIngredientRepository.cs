using Microsoft.EntityFrameworkCore;

namespace RO.Repo;

public class RecipeIngredientRepository : IRecipeIngredientRepository
{
    private readonly AppDbContext _context;

    public RecipeIngredientRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Recipe_Ingredient> GetAll() => _context.Recipes_Ingredients.AsEnumerable();

    public Recipe_Ingredient Get(Guid id) => _context.Recipes_Ingredients.FirstOrDefault(e => e.Id == id) ?? new();
    public string Insert(RecipeIngredientRequest entity)
    {

        Recipe_Ingredient recipeIngredient = new()
        {

            IngredientId = entity.IngredientId,
            RecipeId = entity.RecipeId
        };
        try
        {
            Recipe_Ingredient found = _context.Recipes_Ingredients.Find(entity.Id);
            Recipe recipeFromDb = _context.Recipes.Find(entity.RecipeId);
            Ingredient ingredientFromDb = _context.Ingredients.Find(entity.IngredientId);

            if (found == null)
            {
                if (recipeFromDb != null && ingredientFromDb != null)
                {
                    try
                    {
                        _context.Recipes_Ingredients.Add(recipeIngredient);
                        _context.SaveChanges();

                        return "Suceess Add";
                    }
                    catch
                    {
                        return ("This ingriedient is already in this recipe.");
                    }
                }
                else
                {
                    return "Enter A Correct Id";

                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "This ingredient is already in this recipe.";
    }

    public string Delete(Guid id)
    {
        try
        {
            Recipe_Ingredient dataFromDb = _context.Recipes_Ingredients.FirstOrDefault(e => e.Id == id) ?? new();

            if (dataFromDb != null)
            {
                _context.Recipes_Ingredients.Remove(dataFromDb);
                _context.SaveChanges();
                return ("Delete success");
            }

            return "Not Found !!!!.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string Update(Recipe_Ingredient entity, Guid id)
    {
        try
        {
            Recipe_Ingredient recipe_Ingredient = new()
            {

                IngredientId = entity.IngredientId,
                RecipeId = entity.RecipeId
            };

            Recipe_Ingredient found = _context.Recipes_Ingredients.Find(id) ;

            if (found is not null)
            {
                recipe_Ingredient.Recipe = entity.Recipe;
                recipe_Ingredient.Ingredient = entity.Ingredient;
            }
            else
            {
                return "Please Enter Valid Id";
            }
        }
        catch (Exception)
        {
            throw new InvalidOperationException();
        }

        return "\n Enter valid one !!!";
    }

    public string Remove(Recipe_Ingredient entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Recipes_Ingredients.Remove(entity);
        return "Recipe removed successfully !!!";
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}