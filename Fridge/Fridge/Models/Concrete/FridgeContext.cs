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
  }
}
