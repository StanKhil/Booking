using Booking.Data;
using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Booking.Models.admin;
using Booking.Views;
using System.Collections.ObjectModel;

namespace Booking.ViewModels.admin
{
    public class AdminCreateViewModel : ViewModel
    {
        private string name;
        private string email;
        private string login;
        private string password;
        private string userRole;

        private string? errorMessage = "";
        private bool isViewVisible = true;

        DataContext context = new();
        CreateUserModel userModel;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string UserRole
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

        private string _selectedRole;
        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }



        public event EventHandler OnRequestClose;
        public event EventHandler OnRequestClearUserCreateForm;

        public ICommand MainWindowCommand { get; }
        public ICommand CreateUserCommand { get; }

        public AdminCreateViewModel()
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            CreateUserCommand = new RelayCommand(ExecuteCreateUserCommand);
            this.context = new();
            this.userModel = new(context);
        }

        public AdminCreateViewModel(DataContext context, CreateUserModel model)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            CreateUserCommand = new RelayCommand(ExecuteCreateUserCommand);
            this.context = context;
            this.userModel = model;
        }

        private void ExecuteMainWindowCommand(object obj)
        {
            MainView mainView = new MainView(context);
            mainView.Show();

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteCreateUserCommand(object obj)
        {
            bool success = userModel.CreateUser(name, email, login, password, userRole);
            if (!success)
            {
                ErrorMessage = "Invalid data";
                return;
            }
            /*else
            {
                MainView mainView = new MainView(context);
                mainView.Show();
                OnRequestClose?.Invoke(this, EventArgs.Empty);
            }*/
            OnRequestClearUserCreateForm?.Invoke(this, EventArgs.Empty);
        }
    }
}
