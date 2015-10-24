namespace Fridge.Models.Concrete.Product
{
  public class ProductSearchOutput
  {
    public int ProductID { get; set; }
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public int AmountDefault { get; set; }
    public int UnitMeasureID { get; set; }
  }
}
