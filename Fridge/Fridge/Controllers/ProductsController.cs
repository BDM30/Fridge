using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;
using Fridge.Models.Concrete.Product;
using Ninject;

namespace Fridge.Controllers
{
    public class ProductsController : Controller
    {
      [Inject] private ICommonRepository<Product> dataP;
      [Inject] private ICommonRepository<UserProduct> dataUP;
      [Inject] private ICommonRepository<User> dataU;

    public ProductsController(ICommonRepository<Product> d, ICommonRepository<UserProduct> d2, ICommonRepository<User> d3  )
      {
        dataP = d;
        dataUP = d2;
        dataU = d3;
      }

      public ActionResult GetUserProducts(int id)
      {
        IEnumerable<UserProduct> products = ( from x in dataUP.Data
                                              where x.UserID == id
                                              select x);
        return Json(products, JsonRequestBehavior.AllowGet);
      }
    
      public ActionResult ProductSearch([FromBody]ProductSearchInput input)
      {
        Product product = (from x in dataP.Data
                           where x.Barcode != null && input.Barcode.ToUpper().Equals(x.Barcode.ToUpper())
                           select x).FirstOrDefault();
        if (product == null)
        {
          return Json(null, JsonRequestBehavior.AllowGet);
        }

        ProductSearchOutput res = new ProductSearchOutput()
        {
          AmountDefault = product.AmountDefault,
          CategoryID = product.CategoryID,
          Name = product.Name,
          ProductID = product.ProductID,
          UnitMeasureID = product.UnitMeasureID
        };

        return Json(res, JsonRequestBehavior.AllowGet);
      }

      // [System.Web.Http.HttpPost]
      public ActionResult NewProductAdd([FromBody] NewProductAddInput input)
      {
        dataP.SaveData(new Product()
        {
          AmountDefault = input.AmountDefault,
          Barcode = input.Barcode,
          CategoryID = input.CategoryID,
          Name = input.Name,
          UnitMeasureID = input.UnitMeasureID,
        });

        int idProduct = dataP.Data.Last().ProductID;

        dataUP.SaveData(new UserProduct()
        {
          UserID = input.UserID,
          CategoryID = input.CategoryID,
          Name = input.Name,
          Amount = input.Amount,
          UnitMeasureID = input.UnitMeasureID,
          ExpirationDate = input.ExpirationDate,
          ProductID = idProduct,
          AmountDefault = input.AmountDefault
        });

        return Json(new NewProductAddOutput() { UserProductID = dataUP.Data.Last().UserProductID, ProductID = idProduct }, JsonRequestBehavior.AllowGet);
      }

      public ActionResult AmountChange([FromBody] AmountChangeInput input)
      {
        UserProduct userProduct = (from x in dataUP.Data
                                 where input.UserProductID == x.UserProductID
                                 select x).FirstOrDefault();

        userProduct.Amount = input.Amount;
        dataUP.SaveData(userProduct);
      return Json(new AmountChangeOutput() { IsCorrect = 0 }, JsonRequestBehavior.AllowGet);
    }

      public ActionResult ProductAdd([FromBody] ProductAddInput input)
      {
        Product product = ( from x in dataP.Data
                            where input.ProductID == x.ProductID
                            select x).FirstOrDefault();

        User user = ( from x in dataU.Data
                      where input.UserID == x.UserID
                      select x).FirstOrDefault();

        UserProduct userProduct = new UserProduct
        {
          FridgeID = user.FridgeID,
          UserID = user.UserID,
          ProductID = product.ProductID,
          CategoryID = product.CategoryID,
          Name = product.Name,
          UnitMeasureID = product.UnitMeasureID,
          Amount = input.Amount,
          ExpirationDate = input.ExspirationDate,
          AmountDefault = product.AmountDefault
        };

        dataUP.SaveData(userProduct);
        return Json (new ProductAddOutput {UserProductID = dataUP.Data.Last().UserProductID}, JsonRequestBehavior.AllowGet) ;
      }


  }
}