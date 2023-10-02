namespace RO.Data;

public class FeedBackConfiguration
{
    public FeedBackConfiguration(EntityTypeBuilder<FeedBack> entityTypeBuilder)
    {
        entityTypeBuilder.HasOne(e => e.Recipe).
                WithMany(e => e.FeedBacks).HasForeignKey(e => e.RecipeId);
    }
}