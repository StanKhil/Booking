using Booking.Data;
using Booking.Models;
using Booking.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Booking.Views.Scenes.Sub_Scenes;

namespace Booking.ViewModels.admin
{
    public class UserAdminViewModel : ViewModel
    {
        private string? login;
        private string? password;

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

        DataContext context = new();
        UserModel userModel;

        public ICommand CreateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand UpdateUserCommand { get; }

        public UserAdminViewModel()
        {
            CreateUserCommand = new RelayCommand(ExecuteCreateUserCommand);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUserCommand);
            UpdateUserCommand = new RelayCommand(ExecuteUpdateUserCommand);
            this.context = new();
            this.userModel = new(context);
        }

        public UserAdminViewModel(DataContext context, UserModel model)
        {
            CreateUserCommand = new RelayCommand(ExecuteCreateUserCommand);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUserCommand);
            UpdateUserCommand = new RelayCommand(ExecuteUpdateUserCommand);
            this.context = context;
            this.userModel = model;
        }


        private void ExecuteCreateUserCommand(object? obj)
        {
            CreateUserAdminWindow window = new(context);
            window.Show();
        }

        private void ExecuteDeleteUserCommand(object? obj)
        {
            //bool success = userModel.DeleteUser(login);
            //if (!success)
            //{
            //    ErrorMessage = "Invalid data";
            //    return;
            //}
            //OnRequestClearUserDeleteForm?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteUpdateUserCommand(object? obj)
        {
            //bool success = userModel.UpdateUser(name, email, login, password, userRole);
            //if (!success)
            //{
            //    ErrorMessage = "Invalid data";
            //    return;
            //}
            //OnRequestClearUserUpdateForm?.Invoke(this, EventArgs.Empty);
        }
    }
}
