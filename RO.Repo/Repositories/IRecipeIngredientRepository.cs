namespace RO.Repo;

public interface IRecipeIngredientRepository
{
    IEnumerable<Recipe_Ingredient> GetAll();
    Recipe_Ingredient Get(Guid id);
    string Insert(RecipeIngredientRequest entity);
    string Update(Recipe_Ingredient entity, Guid id);
    string Delete(Guid id);
    string Remove(Recipe_Ingredient entity);
    void SaveChanges();
}