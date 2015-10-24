using System.Linq;
using System.Web.Http;
using Fridge.Controllers;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
  [TestClass]
  public class UserControllerTest
  {
    [TestMethod]
    public void can_get_data()
    {
      // Arrange - create the mock repository
      Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();

      mock.Setup(m => m.Data).Returns(new[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"}
      });
      // Arrange - create a controller
      UsersController target = new UsersController(mock.Object);
      // Action
      User[] result = target.GetUsers().ToArray();
      // Assert
      Assert.AreEqual(result.Length, 3);
      Assert.AreEqual("P1", result[0].Name);
      Assert.AreEqual("P2", result[1].Name);
      Assert.AreEqual("P3", result[2].Name);
    }

    [TestMethod]
    public void can_get_data_one()
    {
      // Arrange - create the mock repository
      Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();

      mock.Setup(m => m.Data).Returns(new[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"}
      });
      // Arrange - create a controller
      UsersController target = new UsersController(mock.Object);
      // Arrange - create a search tag
      int id = 2;
      // Action
      IHttpActionResult actionResult =  target.GetUser(id);
      // Assert
      Assert.IsInstanceOfType(actionResult, typeof(System.Web.Http.Results.OkNegotiatedContentResult<User>));
    }

    //[TestMethod]
    //public void can_save_user()
    //{
    //  // Arrange - create the mock repository
    //  Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();

    //  mock.Setup(m => m.Data).Returns(new[]
    //  {
    //    new User {UserID = 1, Name = "P1"},
    //    new User {UserID = 2, Name = "P2"},
    //    new User {UserID = 3, Name = "P3"}
    //  });
    //  // Arrange - create a controller
    //  UserController target = new UserController(mock.Object);
    //  User user = new User() { UserID = 2, Name = "P33" };
    //  // Act - try to save the product
    //  string result = target.SaveUser(user);
    //  // Assert - check that the repository was called
    //  mock.Verify(m => m.SaveData(user));
    //  // Assert - check the method result type
    //  Assert.IsInstanceOfType(result, typeof(string));
    //}

    //[TestMethod]
    //public void can_remove_user()
    //{
    //  // Arrange - create an User
    //  User prod = new User { UserID = 2, Name = "Test" };
    //  // Arrange - create the mock repository
    //  Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();

    //  mock.Setup(m => m.Data).Returns(new[]
    //  {
    //    new User {UserID = 1, Name = "P1"},
    //    new User {UserID = 2, Name = "P2"},
    //    new User {UserID = 3, Name = "P3"}
    //  });
    //  // Arrange - create a controller
    //  UserController target = new UserController(mock.Object);
    //  // Act - delete the user
    //  target.RemoveUser(prod.UserID);
    //  // Assert - ensure that the repository delete method was
    //  // called with the correct Product
    //  mock.Verify(m => m.DeleteData(prod.UserID));
    //}
  }
}