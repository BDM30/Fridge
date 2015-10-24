using System.Collections.Generic;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;

/*
Класс реализует паттерн Репозиторий те упрощает и инкапсулирует работу с данными из класса FoodContext
*/

namespace Fridge.Models.Concrete.Repositories
{
  public class FridgeRepository : ICommonRepository<Entities.Fridge>
  {
    private FridgeContext context = new FridgeContext();

    public IEnumerable<Entities.Fridge> Data
    {
      get { return context.Fridges; }
    }

    public void SaveData(Entities.Fridge data)
    {
      if (data.FridgeID == 0)
      {
        context.Fridges.Add(data);
      }
      else
      {
        Entities.Fridge dbEntry = context.Fridges.Find(data.FridgeID);
        if (dbEntry != null)
        {
          dbEntry.Name = data.Name;
          //dbEntry.AmountDefault = data.AmountDefault;
          //dbEntry.Barcode = data.Barcode;
          //dbEntry.UnitMeasureID = data.UnitMeasureID;
          //dbEntry.CategoryID = data.CategoryID;
        }
      }
      context.SaveChanges();
    }

    public void DeleteData(int FridgeId)
    {
      Entities.Fridge dbEntry = context.Fridges.Find(FridgeId);
      if (dbEntry != null)
      {
        context.Fridges.Remove(dbEntry);
        context.SaveChanges();
      }
    }
  }
}
