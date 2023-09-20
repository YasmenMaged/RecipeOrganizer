namespace RO.Data;

public class RecipeConfiguration
{
    public RecipeConfiguration(EntityTypeBuilder<Recipe> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(e => e.Id);

        entityTypeBuilder.Property(e => e.Name).IsRequired();
        entityTypeBuilder.Property(e => e.Steps).IsRequired();
    }
}