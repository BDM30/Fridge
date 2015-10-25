using System.Collections.Generic;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;

/*
Класс реализует паттерн Репозиторий те упрощает и инкапсулирует работу с данными из класса FoodContext
*/

namespace Fridge.Models.Concrete.Repositories
{
  public class RecipeRepository : ICommonRepository<Entities.Recipe>
  {
    private FridgeContext context = new FridgeContext();

    public IEnumerable<Entities.Recipe> Data
    {
      get { return context.Recipes; }
    }

    public void SaveData(Entities.Recipe data)
    {
      if (data.RecipeID == 0)
      {
        context.Recipes.Add(data);
      }
      else
      {
        Entities.Recipe dbEntry = context.Recipes.Find(data.RecipeID);
        if (dbEntry != null)
        {
          dbEntry.Name = data.Name;
          dbEntry.ProcessDescription = data.ProcessDescription;
        }
      }
      context.SaveChanges();
    }

    public void DeleteData(int RecipeId)
    {
      Entities.Recipe dbEntry = context.Recipes.Find(RecipeId);
      if (dbEntry != null)
      {
        context.Recipes.Remove(dbEntry);
        context.SaveChanges();
      }
    }
  }
}
