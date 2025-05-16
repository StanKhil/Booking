using Booking.Data;
using Booking.Models;
using Booking.Views;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Models
{
    [TestClass]
    public sealed class UserModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            DataContext context = new DataContext();
            UserModel model = new UserModel(context);
            Assert.IsNotNull(model, "UserModel constructor must initialize non-null DataContext instance");
        }

        [TestMethod]
        public void CrudTest()
        {
            DataContext context = new DataContext();
            UserModel model = new UserModel(context);

            var resultOnCreate = model?.CreateUserAsync("name", "email", "login", "password", "SelfRegistered");
            Assert.IsInstanceOfType<Task<bool>>(resultOnCreate);
            //Assert.IsTrue(resultOnCreate.Result);

            var resultOnUpdate = model?.UpdateUserAsync("nameNew",  "emailNew", "loginNew", "passwordNew", "SelfRegistered", "login");
            Assert.IsInstanceOfType<Task<bool>>(resultOnUpdate);

            var resultOnDelete = model?.DeleteUserAsync("login");
            Assert.IsInstanceOfType<Task<bool>>(resultOnDelete);
        }

        [TestMethod]
        public void RegisterTest()
        {
            DataContext context = new DataContext();
            UserModel model = new UserModel(context);

            var result = model?.RegisterAsync("name", "email", "login", "password");
            Assert.IsInstanceOfType<Task<bool>>(result);
            Assert.IsTrue(result.Result);
        }


    }
}
