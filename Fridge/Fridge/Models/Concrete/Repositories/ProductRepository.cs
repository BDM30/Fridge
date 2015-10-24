using System.Collections.Generic;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;

/*
Класс реализует паттерн Репозиторий те упрощает и инкапсулирует работу с данными из класса FoodContext
*/

namespace Fridge.Models.Concrete.Repositories
{
  public class ProductRepository : ICommonRepository<Entities.Product>
  {
    private FridgeContext context = new FridgeContext();

    public IEnumerable<Entities.Product> Data
    {
      get { return context.Products; }
    }

    public void SaveData(Entities.Product data)
    {
      if (data.ProductID == 0)
      {
        context.Products.Add(data);
      }
      else
      {
        Entities.Product dbEntry = context.Products.Find(data.ProductID);
        if (dbEntry != null)
        {
          dbEntry.Name = data.Name;
          dbEntry.AmountDefault = data.AmountDefault;
          dbEntry.Barcode = data.Barcode;
          dbEntry.UnitMeasureID = data.UnitMeasureID;
          dbEntry.CategoryID = data.CategoryID;
        }
      }
      context.SaveChanges();
    }

    public void DeleteData(int ProductId)
    {
      Entities.Product dbEntry = context.Products.Find(ProductId);
      if (dbEntry != null)
      {
        context.Products.Remove(dbEntry);
        context.SaveChanges();
      }
    }
  }
}
