namespace RO.Data;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime? AddedDate { get; set; }
}