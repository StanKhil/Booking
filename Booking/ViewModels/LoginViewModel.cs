﻿using Booking.Data;
using Booking.Models;
using Booking.Views;
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

        public event EventHandler OnRequestClose;

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
            this.context = new();
            userModel = new UserModel(context);
        }
        public LoginViewModel(DataContext context, UserModel model)
        {
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
            RegisterWindowCommand = new RelayCommand(ExecuteRegisterWindowCommand);
            this.context = context;
            userModel = model;
        }
        private void ExecuteRegisterWindowCommand(object? obj)
        {
            RegisterView register = new(context);
            register.Show();
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
        private async void ExecuteLoginCommand(object? obj)
        {
            bool success = await userModel.LoginAsync(login, password);
            if(!success)
            {
                ErrorMessage = "Invalid login or password";
                return;
            }
            else
            {
                //MessageBox.Show(userModel.userAccess.Login);
                if(userModel.userAccess != null)
                {
                    MainView mainView = new(context, userModel.userAccess);
                    if(userModel.userAccess.RoleId == "SelfRegistered") // TO DO define "admin" somewhere in global scope, I dunno
                    {
                        mainView.adminRadioButton.Height = 0;
                        mainView.adminRadioButton.Margin = new Thickness(0,0,0,0);
                    }
                    mainView.Show();
                    OnRequestClose?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
