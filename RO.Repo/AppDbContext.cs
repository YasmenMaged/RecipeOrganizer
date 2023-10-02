using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RO.Repo;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new RecipeConfiguration(modelBuilder.Entity<Recipe>());
        new FeedBackConfiguration(modelBuilder.Entity<FeedBack>());
        new IngredientConfiguration(modelBuilder.Entity<Ingredient>());
        new RecipeIngredientConfiguration(modelBuilder.Entity<Recipe_Ingredient>());

        SeedRoles(modelBuilder);
    }
    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData
            (
            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
             new IdentityRole() { Name = "HR", ConcurrencyStamp = "3", NormalizedName = "HR" }

            );
    }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe_Ingredient> Recipes_Ingredients { get; set; }
}