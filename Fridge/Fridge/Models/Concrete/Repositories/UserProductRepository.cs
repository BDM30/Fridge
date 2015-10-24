using System.Collections.Generic;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;

/*
Класс реализует паттерн Репозиторий те упрощает и инкапсулирует работу с данными из класса FoodContext
*/

namespace Fridge.Models.Concrete.Repositories
{
  public class UserProductRepository : ICommonRepository<UserProduct>
  {
    private FridgeContext context = new FridgeContext();

    public IEnumerable<UserProduct> Data
    {
      get { return context.UserProducts; }
    }

    public void SaveData(UserProduct data)
    {
      if (data.UserProductID == 0)
      {
        context.UserProducts.Add(data);
      }
      else
      {
        UserProduct dbEntry = context.UserProducts.Find(data.UserProductID);
        if (dbEntry != null)
        {
          dbEntry.Name = data.Name;
          dbEntry.UnitMeasureID = data.UnitMeasureID;
          dbEntry.CategoryID = data.CategoryID;
          dbEntry.Amount = data.Amount;
          dbEntry.ExpirationDate = data.ExpirationDate;
          dbEntry.UserID = data.UserID;
          dbEntry.FridgeID = data.FridgeID;
          dbEntry.ProductID = data.ProductID;
          dbEntry.AmountDefault = data.AmountDefault;
        }
      }
      context.SaveChanges();
    }

    public void DeleteData(int UserProductId)
    {
      UserProduct dbEntry = context.UserProducts.Find(UserProductId);
      if (dbEntry != null)
      {
        context.UserProducts.Remove(dbEntry);
        context.SaveChanges();
      }
    }
  }
}
