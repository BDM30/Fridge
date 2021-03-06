﻿/*
Класс сущности используется для биекции с одноименной таблицей в базе данных ( с помощью Entity Framework)
Отношения всех сущностей смотрите в файле relations.png
*/

namespace Fridge.Models.Concrete.Entities
{
  public class Product
  {
    public int ProductID { get; set; }
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string Barcode { get; set; }
    public int AmountDefault { get; set; }
    public int UnitMeasureID { get; set; }
  }
}
