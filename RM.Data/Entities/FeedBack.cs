namespace RO.Data;

public class FeedBack : BaseEntity
{
    public int Rate { get; set; }

    public string Review { get; set; }

    public Guid RecipeId { get; set; }

    public Recipe? Recipe { get; set; }
}