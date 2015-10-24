using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

     public ProductsController(ICommonRepository<Product> d, ICommonRepository<UserProduct> d2 )
      {
        dataP = d;
        dataUP = d2;
      }
    
      public ActionResult ProductSearch([FromBody]ProductSearchInput input)
      {
        Product product = (from x in dataP.Data
                         where input.Barcode.ToUpper().Equals(x.Barcode.ToUpper())
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
        return Json(new NewProductAddOutput() { UserProductID = dataUP.Data.Last().UserProductID }, JsonRequestBehavior.AllowGet);
      }


  }
}