namespace RO.Data;

public class RecipeIngredientConfiguration
{
    public RecipeIngredientConfiguration(EntityTypeBuilder<Recipe_Ingredient> entityTypeBuilder)
    {
        entityTypeBuilder.HasIndex(e => new { e.RecipeId, e.IngredientId }).IsUnique();

        entityTypeBuilder.HasOne(e => e.Recipe).
            WithMany(e => e.Recipe_Ingredient).HasForeignKey(e => e.RecipeId);

        entityTypeBuilder.HasOne(e => e.Ingredient).
            WithMany(e => e.Recipe_Ingredient).HasForeignKey(e => e.IngredientId);
    }
}