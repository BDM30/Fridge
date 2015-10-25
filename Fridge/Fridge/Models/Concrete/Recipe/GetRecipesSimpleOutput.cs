
using System.Collections.Generic;
using Fridge.Models.Concrete.Entities;

namespace Fridge.Models.Concrete.Recipe
{
  public class GetRecipesSimpleOutput
  {
    public int RecipeID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<IngredientExpanded> Ingredients { get; set; }
    //public IEnumerable<Ingredient> Ingredients { get; set; }

    public int ProductID { get; set; }
  }
}
