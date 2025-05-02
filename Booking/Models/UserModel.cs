using Booking.Data;
using Booking.Data.Entities;
using Booking.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Booking.Models
{
    public class UserModel
    {
        DataContext context;
        public UserAccess? userAccess = null;
        public UserModel(DataContext context)
        {
            this.context = context;
        }
        public bool Login(string login, string password)
        {
            if (context.UserAccesses.Include(ua => ua.User).FirstOrDefault(ua => ua.Login == login && ua.User.DeletedAt == null) is UserAccess access)
            {
                userAccess = access;
                string dk = Crypto.kdf(password, access.Salt);
                if (dk == access.Dk)
                {
                    MessageBox.Show("Signed in", "System", MessageBoxButton.OK, MessageBoxImage.Information);
                    //MainView mainView = new(context, access);
                    //MainView mainView = new();
                    //mainView.Show();
                    //this.Close();
                    return true;
                }
            }
            MessageBox.Show("Access denied", "System", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
}
