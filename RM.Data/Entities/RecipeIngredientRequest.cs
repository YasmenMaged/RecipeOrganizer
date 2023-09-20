namespace RO.Data;

public class RecipeIngredientRequest
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public Guid IngredientId { get; set; }
}