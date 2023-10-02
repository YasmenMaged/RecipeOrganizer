namespace RO.Data;

public class Recipe : BaseEntity
{
    public string? Steps { get; set; }

    public string? Description { get; set; }
    [Required]
    public  string Category { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public  List<Recipe_Ingredient>? Recipe_Ingredient { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public List<FeedBack>? FeedBacks { get; set; }
}