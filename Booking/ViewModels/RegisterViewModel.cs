using Booking.Data;
using Booking.Models;
using System.Windows.Input;
using Booking.Views;
using System.Windows;

namespace Booking.ViewModels
{
    public class RegisterViewModel : ViewModel
    {
        private string name;
        private string email;
        private string login;
        private string password;

        private string? errorMessage = "";
        private bool isViewVisible = true;

        DataContext context = new();
        UserModel userModel;

        public event EventHandler OnRequestClose;

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

        public ICommand RegisterCommand { get; }
        public ICommand LoginWindowCommand { get; }
        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand);
            LoginWindowCommand = new RelayCommand(ExecuteLoginWindowCommand);
            this.context = new();
            this.userModel = new(context);
        }
        public RegisterViewModel(DataContext context, UserModel userModel)
        {
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand);
            LoginWindowCommand = new RelayCommand(ExecuteLoginWindowCommand);
            this.userModel = userModel;
            this.context = context;
        }
        private void ExecuteLoginWindowCommand(object? obj)
        {
            LoginView login = new();
            login.Show();

            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private async void ExecuteRegisterCommand(object? obj)
        {
            MessageBox.Show(name + password + email + login);
            bool success = await userModel.RegisterAsync(name, email, login, password);
            if(!success)
            {
                ErrorMessage = "Invalid data";
                return;
            }
            else
            {
                LoginView loginView = new();
                loginView.Show();
                OnRequestClose?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
