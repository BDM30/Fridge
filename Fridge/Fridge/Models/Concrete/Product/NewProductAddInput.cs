using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridge.Models.Concrete.Product
{
  public class NewProductAddInput
  {
    public int UserID { get; set; }
    public string Barcode { get; set; }
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public int AmountDefault { get; set; }
    public int UnitMeasureID { get; set; }
    public int Amount { get; set; }
    public string ExpirationDate { get; set; }
  }
}
