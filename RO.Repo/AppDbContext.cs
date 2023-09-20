using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RO.Repo;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new RecipeConfiguration(modelBuilder.Entity<Recipe>());
        new IngredientConfiguration(modelBuilder.Entity<Ingredient>());
        new RecipeIngredientConfiguration(modelBuilder.Entity<Recipe_Ingredient>());
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe_Ingredient> Recipes_Ingredients { get; set; }
}