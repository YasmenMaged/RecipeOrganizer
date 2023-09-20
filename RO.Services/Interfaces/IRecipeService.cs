﻿namespace RO.Services;

public interface IRecipeService
{
    IEnumerable<Recipe> GetAllRecipes();

    string AddRecipe(Recipe recipe);

    string UpdateRecipe(Recipe recipe, Guid id);

    public RecipeWithIngredientsVM GetRecipeWithIngredient(Guid id);

    string RemoveRecipe(Guid id);
}