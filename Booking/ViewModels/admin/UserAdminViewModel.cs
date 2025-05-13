using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Booking.ViewModels.admin
{
    public class UserAdminViewModel : ViewModel
    {
        UserAccess access;

        private string? name;
        private string? email;
        private string? login;
        private string? password;
        private string? userRole;

        private string? updateLogin;
        private string? updatePassword;

        private string? newName;
        private string? newEmail;
        private string? newLogin;
        private string? newPassword;
        private string? newRole;

        private string? errorMessageOnCreate = "";
        private string? errorMessageOnUpdate = "";
        private string? errorMessageOnDelete = "";
        private bool isViewVisible = true;

        DataContext context = new();
        UserModel userModel;

        public string? Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string? Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string? Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string? Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string? UserRole
        {
            get => userRole;
            set
            {
                userRole = value;
                OnPropertyChanged(nameof(UserRole));
            }
        }
        public string? UpdateLogin
        {
            get => updateLogin;
            set
            {
                updateLogin = value;
                OnPropertyChanged(nameof(UpdateLogin));
            }
        }
        public string? UpdatePassword
        {
            get => updatePassword;
            set
            {
                updatePassword = value;
                OnPropertyChanged(nameof(UpdatePassword));
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
        public string? NewRole
        {
            get => newRole;
            set
            {
                newRole = value;
                OnPropertyChanged(nameof(NewRole));
            }
        }
        public string? ErrorMessageOnCreate
        {
            get => errorMessageOnCreate;
            set
            {
                errorMessageOnCreate = value;
                OnPropertyChanged(nameof(ErrorMessageOnCreate));
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

        public ObservableCollection<string> Roles { get; } = new ObservableCollection<string>
        {
            "Administrator",
            "Employee",
            "Moderator",
            "SelfRegistered"
        };

        private string? _selectedRole;
        public string? SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //public event EventHandler? OnRequestClose;
        //public event EventHandler? OnRequestClearUserCreateForm;
        //public event EventHandler? OnRequestClearUserDeleteForm;
        //public event EventHandler? OnRequestClearUserUpdateForm;

        public ICommand CreateUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public UserAdminViewModel(UserAccess access)
        {
            CreateUserCommand = new RelayCommand(ExecuteCreateUserCommand);
            UpdateUserCommand = new RelayCommand(ExecuteUpdateUserCommand);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUserCommand);
            this.context = new();
            this.userModel = new(context);
            this.access = access;
        }
        public UserAdminViewModel(DataContext context, UserModel model)
        {
            CreateUserCommand = new RelayCommand(ExecuteCreateUserCommand);
            UpdateUserCommand = new RelayCommand(ExecuteUpdateUserCommand);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUserCommand);
            this.context = context;
            this.userModel = model;
        }
        private async void ExecuteCreateUserCommand(object? obj)
        {
            var canCreate = await context.UserRoles
                .Where(r => r.Id == access.RoleId)
                .Select(r => r.CanCreate)
                .FirstOrDefaultAsync();

            if (!canCreate)
            {
                ErrorMessageOnCreate = "You do not have permission to create realty.";
                return;
            }

            bool success = await userModel.CreateUserAsync(name, email, login, password, userRole);
            if (!success)
            {
                ErrorMessageOnCreate = "Invalid data";
                return;
            }
            //OnRequestClearUserCreateForm?.Invoke(this, EventArgs.Empty);
        }
        private async void ExecuteUpdateUserCommand(object? obj)
        {
            var canUpdate = await context.UserRoles
                .Where(r => r.Id == access.RoleId)
                .Select(r => r.CanUpdate)
                .FirstOrDefaultAsync();

            if (!canUpdate)
            {
                ErrorMessageOnUpdate = "You do not have permission to create realty.";
                return;
            }

            bool success = await userModel.UpdateUserAsync(newName, newEmail, newLogin, newPassword, newRole, login);
            if (!success)
            {
                ErrorMessageOnUpdate = "Invalid data";
                return;
            }
            //OnRequestClearUserUpdateForm?.Invoke(this, EventArgs.Empty);
        }
        private async void ExecuteDeleteUserCommand(object? obj)
        {
            var canDelete = await context.UserRoles
                .Where(r => r.Id == access.RoleId)
                .Select(r => r.CanDelete)
                .FirstOrDefaultAsync();

            if (!canDelete)
            {
                ErrorMessageOnDelete = "You do not have permission to create realty.";
                return;
            }

            bool success = await userModel.DeleteUserAsync(updateLogin);
            if (!success)
            {
                ErrorMessageOnDelete = "Invalid data";
                return;
            }
            //OnRequestClearUserDeleteForm?.Invoke(this, EventArgs.Empty);
        }
    }
}
