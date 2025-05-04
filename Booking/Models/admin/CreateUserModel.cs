using Booking.Data.Entities;
using Booking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Booking.Models.admin
{
    public class CreateUserModel
    {
        DataContext context;
        public UserAccess? userAccess = null;
        public CreateUserModel(DataContext context)
        {
            this.context = context;
        }

        public bool CreateUser(string name, string email, string login, string password, string userRole)
        {
            if (context.UserAccesses.Any(ua => ua.Login == login && ua.User.DeletedAt == null))
            {
                System.Windows.MessageBox.Show("Login already exists");
                return false;
            }

            Guid userId = Guid.NewGuid();
            User user = new()
            {
                Id = userId,
                Name = name,
                Email = email,
                RegisteredAt = DateTime.Now,
            };
            String salt = Random.Shared.Next().ToString();
            UserAccess userAccess = new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Login = login,
                Salt = salt,
                Dk = Crypto.kdf(salt, password),
                RoleId = userRole
            };

            context.Users.Add(user);
            context.UserAccesses.Add(userAccess);
            context.SaveChanges();

            System.Windows.MessageBox.Show("Created", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }
    }
}
