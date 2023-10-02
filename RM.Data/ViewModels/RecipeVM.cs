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

public class RecipeWithFeedBacks
{
    public required string Name { set; get; }

    public required List<int> Rates { get; set; }
    public required List<string> Reviews { get; set; }
}