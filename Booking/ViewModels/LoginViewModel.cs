using Booking.Data;
using Booking.Models;
using Booking.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Booking.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        private string login;
        private string password;
        private string? errorMessage = "";
        private bool isViewVisible = true;

        DataContext context = new();
        UserModel userModel;

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

        // Commands
        public ICommand LoginCommand { get; }
        public ICommand RegisterWindowCommand { get; }


        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
            RegisterWindowCommand = new RelayCommand(ExecuteRegisterWindowCommand);
            userModel = new UserModel(context);
        }
        private void ExecuteRegisterWindowCommand(object? obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteLoginCommand(object? obj)
        {
            bool success = userModel.Login(login, password);
            if(!success)
            {
                ErrorMessage = "Invalid login or password";
                return;
            }
            IsViewVisible = false;
        }
    }
}
