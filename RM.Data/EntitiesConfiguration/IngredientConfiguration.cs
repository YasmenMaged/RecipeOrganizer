namespace RO.Data;

public class IngredientConfiguration
{
    public IngredientConfiguration(EntityTypeBuilder<Ingredient> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(e => e.Id);

        entityTypeBuilder.Property(e => e.Name).IsRequired();
    }
}