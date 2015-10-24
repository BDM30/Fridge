using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;
using Fridge.Models.Concrete.Product;

namespace Fridge.Controllers
{
    public class ProductsController : ApiController
    {
      private ICommonRepository<Product> data;

      public ProductsController(ICommonRepository<Product> d)
      {
        data = d;
      }

    [ResponseType(typeof(ProductSearchOutput))]
    public IHttpActionResult ProductSearch(string bar)
    {

      Product product = (from x in data.Data
                         where bar == x.Barcode
                         select x).FirstOrDefault();

      if (product == null)
      {
        return NotFound();
      }

      ProductSearchOutput res = new ProductSearchOutput()
      {
        AmountDefault = product.AmountDefault,
        CategoryID = product.CategoryID,
        Name = product.Name,
        ProductID = product.ProductID,
        UnitMeasureID = product.UnitMeasureID
      };

      return Ok(res);
       
    }


  }
}
