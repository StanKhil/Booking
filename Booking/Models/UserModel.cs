using Booking.Data;
using Booking.Data.Entities;
using Booking.Services;
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
using FontAwesome.Sharp;

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
        public async Task<bool> LoginAsync(string login, string password)
        {
            var userAccess = await context.UserAccesses
                .Include(ua => ua.User)
                .FirstOrDefaultAsync(ua => ua.Login == login && ua.User.DeletedAt == null);

            if (userAccess == null)
            {
                CustomMessageBox.Show("Login Not Found", "System", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return false;
            }

            string dk = Crypto.kdf(userAccess.Salt, password);
            if (dk != userAccess.Dk)
            {
                System.Windows.MessageBox.Show("Wrong password");
                return false;
            }

            this.userAccess = userAccess;
            //System.Windows.MessageBox.Show($"Welcome {userAccess.User.Name}");
            CustomMessageBox.Show("System", $"Welcome {userAccess.User.Name}", MessageBoxButton.OK, IconChar.CircleInfo);
            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            if (this.userAccess == null) return false;
            this.userAccess = null;
            return true;
        }

        public async Task<bool> RegisterAsync(string? name, string? email, string? login, string? password)
        {
            if (await context.UserAccesses.AnyAsync(ua => ua.Login == login && ua.User.DeletedAt == null))
            {
                CustomMessageBox.Show("System", "Login already exists", MessageBoxButton.OK, IconChar.CircleExclamation);
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

            string salt = Random.Shared.Next().ToString();
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
            await context.SaveChangesAsync();

            CustomMessageBox.Show("System", "Registered", MessageBoxButton.OK, IconChar.CircleInfo);
            return true;
        }

        public async Task<bool> CreateUserAsync(string? name, string? email, string? login, string? password, string? userRole)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(userRole))
            {
                if (await context.UserAccesses.AnyAsync(ua => ua.Login == login /*&& ua.User.DeletedAt == null*/))
                {
                    CustomMessageBox.Show("System", "Login already exists", MessageBoxButton.OK, IconChar.CircleExclamation);
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

                string salt = Random.Shared.Next().ToString();
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
                await context.SaveChangesAsync();

                CustomMessageBox.Show("System", "Created", MessageBoxButton.OK, IconChar.CircleInfo);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserAsync(string? nameNew, string? emailNew, string? loginNew, string? passwordNew, string? userRoleNew, string? login)
        {
            //System.Windows.MessageBox.Show(login);
            var userAccess = await context.UserAccesses
                .Include(ua => ua.User)
                .FirstOrDefaultAsync(ua => ua.Login == login && ua.User.DeletedAt == null);

            if (userAccess == null)
            {
                CustomMessageBox.Show("System", "User not found", MessageBoxButton.OK, IconChar.CircleExclamation);
                return false;
            }

            if (!string.IsNullOrEmpty(loginNew))
            {
                if (await context.UserAccesses.AnyAsync(ua => ua.Login == loginNew && ua.User.DeletedAt == null) && login!=loginNew)
                {
                    CustomMessageBox.Show("System", "Login already exists", MessageBoxButton.OK, IconChar.CircleExclamation);
                    return false;
                }
                userAccess.Login = loginNew;
            }
            if (!string.IsNullOrEmpty(nameNew)) userAccess.User.Name = nameNew;
            if (!string.IsNullOrEmpty(emailNew)) userAccess.User.Email = emailNew;
            if (!string.IsNullOrEmpty(passwordNew))
            {
                string salt = Random.Shared.Next().ToString();
                userAccess.Salt = salt;
                userAccess.Dk = Crypto.kdf(salt, passwordNew);
            }
            if (!string.IsNullOrEmpty(userRoleNew)) userAccess.RoleId = userRoleNew;

            await context.SaveChangesAsync();

            CustomMessageBox.Show("System", "Updated", MessageBoxButton.OK, IconChar.CircleInfo);
            return true;
        }

        public async Task<bool> DeleteUserAsync(string? login)
        {
            var userAccess = await context.UserAccesses
                .Include(ua => ua.User)
                .FirstOrDefaultAsync(ua => ua.Login == login && ua.User.DeletedAt == null);

            if (userAccess == null)
            {
                CustomMessageBox.Show("System", "User not found", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return false;
            }

            userAccess.User.DeletedAt = DateTime.Now;
            await context.SaveChangesAsync();

            CustomMessageBox.Show("Deleted", "System", MessageBoxButton.OK, IconChar.CircleInfo);
            return true;
        }

        public async Task<List<BookingItem>> GetBookingsAsync(UserAccess userAccess)
        {
            if (userAccess == null)
            {
                CustomMessageBox.Show("System", "User not found", MessageBoxButton.OK, IconChar.ExclamationCircle);
                return new List<BookingItem>();
            }
            var bookings = await context.BookingItems
                .Include(b => b.Realty)
                .Where(b => b.UserAccessId == userAccess.Id && b.DeletedAt == null)
                .ToListAsync();
            if (bookings.Count == 0)
            {
                CustomMessageBox.Show("System", "No bookings found", MessageBoxButton.OK, IconChar.ExclamationCircle);
            }
            return bookings;
        }
    }
}
