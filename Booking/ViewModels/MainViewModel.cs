using Booking.Data;
using Booking.Models;
using Booking.Views.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Booking.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public DataContext context;
        private string? errorMessage = "";
        private bool isViewVisible = true;

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

        public ICommand CreateWindowCommand { get; }
        public ICommand UpdateWindowCommand { get; }
        public ICommand DeleteWindowCommand { get; }

        public MainViewModel()
        {
            CreateWindowCommand = new RelayCommand(ExecuteCreateWindowCommand);
            UpdateWindowCommand = new RelayCommand(ExecuteUpdateWindowCommand);
            DeleteWindowCommand = new RelayCommand(ExecuteDeleteWindowCommand);
            this.context = new();
        }

        public MainViewModel(DataContext context, UserModel model)
        {
            CreateWindowCommand = new RelayCommand(ExecuteCreateWindowCommand);
            UpdateWindowCommand = new RelayCommand(ExecuteUpdateWindowCommand);
            DeleteWindowCommand = new RelayCommand(ExecuteDeleteWindowCommand);
            this.context = context;
        }

        private void ExecuteCreateWindowCommand(object obj)
        {
            AdminCreateView adminCreateView = new AdminCreateView(context);
            adminCreateView.Show();

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteUpdateWindowCommand(object obj)
        {
            /*AdminUpdateView adminUpdateView = new AdminUpdateView(context);
            adminUpdateView.Show();*/

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteDeleteWindowCommand(object obj)
        {
            /*AdminDeleteView adminDeleteView = new AdminDeleteView(context);
             adminDeleteView.Show();*/

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
