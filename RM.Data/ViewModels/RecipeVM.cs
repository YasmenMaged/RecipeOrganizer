namespace RO.Data;

public class RecipeVM
{
    public required string Name { get; set; }
}

public class RecipeWithIngredientsVM
{
    public required string Name { set; get; }

    public required List<string> IngredientName { get; set; }
}