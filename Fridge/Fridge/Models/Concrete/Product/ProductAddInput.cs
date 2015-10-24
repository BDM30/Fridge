namespace Fridge.Models.Concrete.Product
{
  public class ProductAddInput
  {
    public int ProductID { get; set; }
    public int Amount { get; set; }
    public string ExspirationDate { get; set; }
    public int UserID { get; set; }
  }
}
