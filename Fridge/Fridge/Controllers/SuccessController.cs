using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;
using Fridge.Models.Concrete.Product;
using Ninject;

namespace Fridge.Controllers
{
    public class SuccessController : Controller
    {

     [Inject]
      private ICommonRepository<Product> data;

     public SuccessController(ICommonRepository<Product> d)
      {
        data = d;
      }
    
      public ActionResult Getproducts(string bar)
      {
      Product product = (from x in data.Data
                         where bar.ToUpper().Equals(x.Barcode.ToUpper())
                         select x).FirstOrDefault();
        if (product == null)
        {
          return Json(null);
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


  }
}