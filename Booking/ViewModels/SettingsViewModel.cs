using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using Booking.Views;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;

namespace Booking.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        private string? login;
        private string? newName;
        private string? newEmail;
        private string? newLogin;
        private string? newPassword;
        private string? role;

        private string? errorMessageOnUpdate = "";
        private string? errorMessageOnDelete = "";
        private bool isViewVisible = true;

        DataContext context = new();
        UserModel userModel;
        UserAccess userAccess;

        public string? Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string? NewName
        {
            get => newName;
            set
            {
                newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }
        public string? NewEmail
        {
            get => newEmail;
            set
            {
                newEmail = value;
                OnPropertyChanged(nameof(NewEmail));
            }
        }
        public string? NewLogin
        {
            get => newLogin;
            set
            {
                newLogin = value;
                OnPropertyChanged(nameof(NewLogin));
            }
        }
        public string? NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }
        public string? Role
        {
            get => role;
            set
            {
                role = value;
                OnPropertyChanged(nameof(Role));
            }
        }
        public string? ErrorMessageOnUpdate
        {
            get => errorMessageOnUpdate;
            set
            {
                errorMessageOnUpdate = value;
                OnPropertyChanged(nameof(ErrorMessageOnUpdate));
            }
        }
        public string? ErrorMessageOnDelete
        {
            get => errorMessageOnDelete;
            set
            {
                errorMessageOnDelete = value;
                OnPropertyChanged(nameof(ErrorMessageOnDelete));
            }
        }
        public bool IsViewVisible
        {
            get => isViewVisible;
            set
            {
                isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public ICommand UpdateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public event EventHandler OnRequestClose;

        public SettingsViewModel(UserAccess userAccess)
        {
            this.userAccess = userAccess;
            context = new();
            userModel = new(context);
            UpdateUserCommand = new RelayCommand(ExecuteUpdateUserCommand);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUserCommand);
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userAccess.UserId);
            if (user != null)
            {
                Login = userAccess.Login;
                NewName = user.Name;
                NewEmail = user.Email;
                NewLogin = userAccess.Login;
                Role = userAccess.RoleId;
            }
        }


        public async void ExecuteUpdateUserCommand(object? obj)
        {
            if (await userModel.UpdateUserAsync(NewName, NewEmail, NewLogin, NewPassword, Role, Login))
            {
                IsViewVisible = false;
            }
            else
            {
                ErrorMessageOnUpdate = "Error updating user";
            }
        }

        public async void ExecuteDeleteUserCommand(object? obj)
        {
            if (await userModel.DeleteUserAsync(Login))
            {
                MessageBox.Show("User deleted successfully", "System", MessageBoxButton.OK, MessageBoxImage.Information);
                OnRequestClose?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessageOnDelete = "Error deleting user";
            }
        }
    }
}
