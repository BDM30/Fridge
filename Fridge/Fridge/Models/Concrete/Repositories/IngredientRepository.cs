using System.Collections.Generic;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;

/*
Класс реализует паттерн Репозиторий те упрощает и инкапсулирует работу с данными из класса FoodContext
*/

namespace Fridge.Models.Concrete.Repositories
{
  public class IngredientRepository : ICommonRepository<Entities.Ingredient>
  {
    private FridgeContext context = new FridgeContext();

    public IEnumerable<Entities.Ingredient> Data
    {
      get { return context.Ingredients; }
    }

    public void SaveData(Entities.Ingredient data)
    {
      if (data.IngredientID == 0)
      {
        context.Ingredients.Add(data);
      }
      else
      {
        Entities.Ingredient dbEntry = context.Ingredients.Find(data.IngredientID);
        if (dbEntry != null)
        {
          dbEntry.CategoryID = data.CategoryID;
          dbEntry.Amount = data.Amount;
          dbEntry.ImportanceLevelID = data.ImportanceLevelID;
          dbEntry.RecipeID = data.RecipeID;
        }
      }
      context.SaveChanges();
    }

    public void DeleteData(int IngredientId)
    {
      Entities.Ingredient dbEntry = context.Ingredients.Find(IngredientId);
      if (dbEntry != null)
      {
        context.Ingredients.Remove(dbEntry);
        context.SaveChanges();
      }
    }
  }
}
