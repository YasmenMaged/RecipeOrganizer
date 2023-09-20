namespace RO.Services;

public interface IIngredientService
{
    IEnumerable<Ingredient> GetAllIngredients();

    Ingredient GetIngredientById(Guid id);

    string AddIngredient(Ingredient ingredient);

    string UpdateIngredient(Ingredient ingredient, Guid id);

    string RemoveIngredient(Guid id);
}