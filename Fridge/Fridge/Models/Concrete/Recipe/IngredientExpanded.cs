using Fridge.Models.Concrete.Entities;

namespace Fridge.Models.Concrete.Recipe
{
  public class IngredientExpanded
  {
    public int IngredientID { get; set; }
    public int CategoryID { get; set; }
    public int RecipeID { get; set; }
    public int ImportanceLevelID { get; set; }
    public int Amount { get; set; }
    public int UserProductID { get; set; }
  }
}
