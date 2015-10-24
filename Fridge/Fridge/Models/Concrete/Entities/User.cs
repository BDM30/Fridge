/*
Класс сущности используется для биекции с одноименной таблицей в базе данных ( с помощью Entity Framework)
Отношения всех сущностей смотрите в файле relations.png
*/

namespace Fridge.Models.Concrete.Entities
{
  public class User
  {
    public int UserID { get; set; }

    public int FridgeID { get; set; }

    public string Name { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public int SessionID { get; set; }
  }
}
