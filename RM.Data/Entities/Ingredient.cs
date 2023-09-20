namespace RO.Data;

public class Ingredient : BaseEntity
{
    [JsonIgnore]
    [IgnoreDataMember]
    public List<Recipe_Ingredient>? Recipe_Ingredient { get; set; }
}