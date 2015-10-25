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

      List <Recipe> goodRecipes = new List<Recipe>();

      // k v
      Dictionary<int, int> ingredientUserProduct = new Dictionary<int, int>();

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
            where x.CategoryID == ingredient.CategoryID && x.Amount >= ingredient.Amount
            select x).FirstOrDefault();
          if (userProduct == null)
          {
            resipe_is_good = false;
            break;
          }
          else
          {
            if (! ingredientUserProduct.ContainsKey(ingredient.IngredientID))
              ingredientUserProduct.Add(ingredient.IngredientID, userProduct.UserProductID);
          }    
        }
        if (resipe_is_good)
          goodRecipes.Add(recipe);
      }

      List<GetRecipesSimpleOutput> result = new List<GetRecipesSimpleOutput>();
      foreach (var recipe in goodRecipes)
      {
        IEnumerable<Ingredient> ingredients = (
          from x in dataIngredients.Data
          where x.RecipeID == recipe.RecipeID
          select x);
        List<IngredientExpanded> final_recipes = new List<IngredientExpanded>();
        foreach (var x in ingredients)
        {
          final_recipes.Add(new IngredientExpanded()
          {
            Amount = x.Amount,
            IngredientID = x.IngredientID,
            CategoryID = x.CategoryID,
            ImportanceLevelID =  x.ImportanceLevelID,
            RecipeID = x.RecipeID,
            UserProductID = ingredientUserProduct[x.IngredientID]
          });
        }


        result.Add(new GetRecipesSimpleOutput()
        {
          Description =  recipe.ProcessDescription,
          Name = recipe.Name,
          RecipeID = recipe.RecipeID,
          Ingredients = final_recipes
        });
      }
      return Json((IEnumerable<GetRecipesSimpleOutput>) result, JsonRequestBehavior.AllowGet);
    }

  }
}