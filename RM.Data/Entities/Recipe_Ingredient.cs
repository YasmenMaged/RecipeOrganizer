namespace RO.Data;

public class Recipe_Ingredient
{
    public Guid Id { get; set; }
    
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }
}