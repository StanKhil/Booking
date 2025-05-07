using Booking.Data;
using Booking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Booking.ViewModels.admin
{
    public class CreateUserAdminViewModel : ViewModel
    {
        private string? name;
        private string? email;
        private string? login;
        private string? password;
        private string? userRole;

        private string? errorMessage = "";
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

        public string? ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
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

        public event EventHandler? OnRequestClose;
        public event EventHandler? OnRequestClearUserCreateForm;
        public event EventHandler? OnRequestClearUserDeleteForm;
        public event EventHandler? OnRequestClearUserUpdateForm;

        public ICommand CreateUserCommand { get; }

        public CreateUserAdminViewModel()
        {
            CreateUserCommand = new RelayCommand(ExecuteCreateUserCommand);
            this.context = new();
            this.userModel = new(context);
        }
        public CreateUserAdminViewModel(DataContext context, UserModel model)
        {
            CreateUserCommand = new RelayCommand(ExecuteCreateUserCommand);
            this.context = context;
            this.userModel = model;
        }
        private void ExecuteCreateUserCommand(object? obj)
        {
            bool success = userModel.CreateUser(name, email, login, password, userRole);
            if (!success)
            {
                ErrorMessage = "Invalid data";
                return;
            }
            OnRequestClearUserCreateForm?.Invoke(this, EventArgs.Empty);
        }



    }
}
