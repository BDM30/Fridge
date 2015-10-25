using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;
using Fridge.Models.Concrete.Recipe;
using Ninject;

namespace Fridge.Controllers
{
    public class RecipesController : Controller
    {
      [Inject] private ICommonRepository<User> dataUsers;
      [Inject] private ICommonRepository<UserProduct> dataUserProducts;
      [Inject] private ICommonRepository<Recipe> dataRecipes;
      [Inject] private ICommonRepository<Ingredient> dataIngredients;

      public RecipesController(ICommonRepository<User> d2, ICommonRepository<UserProduct> d3, 
        ICommonRepository<Recipe> d4, ICommonRepository<Ingredient> d5)
      {
        dataUsers = d2;
        dataUserProducts = d3;
        dataRecipes = d4;
        dataIngredients = d5;
      }


    // id user
    public ActionResult GetRecipesSimple(int idUser)
    {
      int fridgeId = (
        from x in dataUsers.Data
        where x.UserID == idUser
        select x.FridgeID).First();

      IEnumerable<UserProduct> userProducts = (
        from x in dataUserProducts.Data
        where fridgeId == x.FridgeID
        select x);

      //
      List<UserProduct> up1 = new List<UserProduct>();
      foreach (var x in userProducts)
      {
        up1
      }


      List <Recipe> goodRecipes = new List<Recipe>();

      // идем по рецептам
      foreach (var recipe in dataRecipes.Data)
      { 
        // получаем список игридиентов
        IEnumerable <Ingredient> ingredients = (
          from x in dataIngredients.Data
          where x.RecipeID == recipe.RecipeID
          select x);

        bool resipe_is_good = true;
        // теперь сопоставим с продуктами Пользователя, годно ли количество и есть ли категории
        foreach (var ingredient in ingredients)
        {
          UserProduct userProduct = (
            from x in dataUserProducts.Data
            where x.CategoryID == ingredient.CategoryID && x.Amount <= ingredient.Amount
            select x).FirstOrDefault();
          if (userProduct == null)
          {
            resipe_is_good = false;
            break;
          }    
        }
        if (resipe_is_good)
          goodRecipes.Add(recipe);
      }

      List<GetRecipesSimpleOutput> result = new List<GetRecipesSimpleOutput>();
      foreach (var recipe in goodRecipes)
      {
        result.Add(new GetRecipesSimpleOutput()
        {
          Description =  recipe.ProcessDescription,
          Name = recipe.Name,
          RecipeID = recipe.RecipeID,
          Ingredients = (from x in dataIngredients.Data
                         where x.RecipeID == recipe.RecipeID
                         select x)
        });
      }
      return Json((IEnumerable<GetRecipesSimpleOutput>) result, JsonRequestBehavior.AllowGet);
    }

  }
}