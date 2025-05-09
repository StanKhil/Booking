﻿using Booking.Data;
using Booking.Data.Entities;
using Booking.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Booking.Models
{
    public class UserModel
    {
        DataContext context;
        public UserAccess? userAccess;
        public UserModel(DataContext context)
        {
            this.context = context;
        }
        public bool Login(string login, string password)
        {
            var userAccess = context.UserAccesses
               .Include(ua => ua.User)
               .FirstOrDefault(ua => ua.Login == login && ua.User.DeletedAt == null);
            if (userAccess == null)
            {
                System.Windows.MessageBox.Show("Login Not Found", "System", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.userAccess = userAccess;
                //System.Windows.MessageBox.Show(this.userAccess.Login);
                return false;
            }
            String dk = Crypto.kdf(userAccess.Salt, password);
            if (dk != userAccess.Dk)
            {
                System.Windows.MessageBox.Show("Wrong password");
                return false;
            }
            this.userAccess = userAccess;
            //System.Windows.MessageBox.Show(this.userAccess.Login);
            System.Windows.MessageBox.Show($"Welcome {userAccess.User.Name}");

            return true;
        }

        public bool Register(string? name, string? email, string? login, string? password)
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
                RoleId = "SelfRegistered"
            };

            context.Users.Add(user);
            context.UserAccesses.Add(userAccess);
            context.SaveChanges();

            System.Windows.MessageBox.Show("Registered", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public bool CreateUser(string? name, string? email, string? login, string? password, string? userRole)
        {
            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(userRole))
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
            return false;
        }

        public bool UpdateUser(string? nameNew, string? emailNew, string? loginNew, string? passwordNew, string? userRoleNew, string? login)
        {
            var userAccess = context.UserAccesses
                .Include(ua => ua.User)
                .FirstOrDefault(ua => ua.Login == login && ua.User.DeletedAt == null);
            if (userAccess == null)
            {
                System.Windows.MessageBox.Show("User not found");
                return false;
            }

            if (!String.IsNullOrEmpty(loginNew))
            {
                if (context.UserAccesses.Any(ua => ua.Login == login && ua.User.DeletedAt == null))
                {
                    System.Windows.MessageBox.Show("Login already exists");
                    return false;
                }
                userAccess.Login = loginNew;
            }
            if (!String.IsNullOrEmpty(nameNew)) userAccess.User.Name = nameNew;
            if(!String.IsNullOrEmpty(emailNew)) userAccess.User.Email = emailNew;
            if (!String.IsNullOrEmpty(passwordNew))
            {
                String salt = Random.Shared.Next().ToString();
                userAccess.Salt = salt;
                userAccess.Dk = Crypto.kdf(salt, passwordNew);
            }
            if (!String.IsNullOrEmpty(userRoleNew)) userAccess.RoleId = userRoleNew;
            context.SaveChanges();

            System.Windows.MessageBox.Show("Updated", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public bool DeleteUser(string? login)
        {
            var userAccess = context.UserAccesses
                .Include(ua => ua.User)
                .FirstOrDefault(ua => ua.Login == login && ua.User.DeletedAt == null);
            if (userAccess == null)
            {
                System.Windows.MessageBox.Show("User not found");
                return false;
            }
            userAccess.User.DeletedAt = DateTime.Now;
            context.SaveChanges();
            System.Windows.MessageBox.Show("Deleted", "System", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }
    }
}
