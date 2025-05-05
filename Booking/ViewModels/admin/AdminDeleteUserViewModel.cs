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

namespace Booking.ViewModels.admin
{
    public class AdminDeleteUserViewModel : ViewModel
    {
        private string login;

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



        public event EventHandler OnRequestClose;
        public event EventHandler OnRequestClearUserDeleteForm;

        public ICommand MainWindowCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public AdminDeleteUserViewModel()
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUserCommand);
            this.context = new();
            this.userModel = new(context);
        }

        public AdminDeleteUserViewModel(DataContext context, UserModel model)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            DeleteUserCommand = new RelayCommand(ExecuteDeleteUserCommand);
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

        private void ExecuteDeleteUserCommand(object obj)
        {
            bool success = userModel.DeleteUser(login);
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
            OnRequestClearUserDeleteForm?.Invoke(this, EventArgs.Empty);
        }
    }
}
