using Booking.Data;
using Booking.Models;
using Booking.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Booking.ViewModels.admin
{
    public class AdminDeleteRealtyViewModel : ViewModel
    {
        private string slug;

        private string? errorMessage = "";
        private bool isViewVisible = true;

        DataContext context = new();
        RealtyModel realtyModel;

        public string Slug
        {
            get => slug;
            set
            {
                slug = value;
                OnPropertyChanged(nameof(Slug));
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
        public event EventHandler OnRequestClearRealtyDeleteForm;

        public ICommand MainWindowCommand { get; }
        public ICommand DeleteRealtyCommand { get; }

        public AdminDeleteRealtyViewModel()
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            DeleteRealtyCommand = new RelayCommand(ExecuteDeleteRealtyCommand);
            this.context = new();
            this.realtyModel = new(context);
        }

        public AdminDeleteRealtyViewModel(DataContext context, RealtyModel model)
        {
            MainWindowCommand = new RelayCommand(ExecuteMainWindowCommand);
            DeleteRealtyCommand = new RelayCommand(ExecuteDeleteRealtyCommand);
            this.context = context;
            this.realtyModel = model;
        }

        private void ExecuteMainWindowCommand(object obj)
        {
            MainView mainView = new MainView(context);
            mainView.Show();
            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteDeleteRealtyCommand(object obj)
        {
            bool success = realtyModel.DeleteRealty(slug);
            if (!success)
            {
                ErrorMessage = "Invalid slug";
                return;
            }
            OnRequestClearRealtyDeleteForm?.Invoke(this, EventArgs.Empty);
        }
    }

}
