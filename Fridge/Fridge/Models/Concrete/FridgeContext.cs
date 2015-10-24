using System.Data.Entity;
using Fridge.Models.Concrete.Entities;

namespace Fridge.Models.Concrete
{

  public class FridgeContext : DbContext
  {
    public FridgeContext(): base("FridgeDB")
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Caterories { get; set; }
    public DbSet<Entities.Fridge> Fridges { get; set; }
    public DbSet<ImportanceLevel> ImportanceLevels { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<RecipeTag> RecipeTags { get; set; }

    public DbSet<ReplaceOption> ReplaceOptions { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<UnitMeasure> UnitsMeasure { get; set; }

    public DbSet<UserProduct> UserProducts { get; set; }

  }
}
