using Booking.Data;
using Booking.Models;
using Booking.Views.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FontAwesome.Sharp;
using System.Windows.Controls;
using Booking.Views.Scenes;


namespace Booking.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public DataContext context;
        string content = "Home";
        IconChar topIcon = new();
        ContentControl sceneContainer = new();

        HomeScene home = new();
        Scheduled scheduled = new();
        Catalogue catalogue = new();
        Settings settings = new();

        private string? errorMessage = "";
        private bool isViewVisible = true;

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public ContentControl SceneContainer
        {
            get => sceneContainer;
            set
            {
                sceneContainer = value;
                OnPropertyChanged(nameof(SceneContainer));
            }
        }
        public IconChar TopIcon
        {
            get => topIcon;
            set
            {
                topIcon = value;
                OnPropertyChanged(nameof(TopIcon));
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

        // Commands
        public ICommand HomeChecked { get; }
        public ICommand ScheduledChecked { get; }
        public ICommand CatalogueChecked { get; }
        public ICommand SettingsChecked { get; }

        public ICommand CreateWindowCommand { get; }
        public ICommand UpdateWindowCommand { get; }
        public ICommand DeleteWindowCommand { get; }
     
        public MainViewModel()
        {
            HomeChecked = new RelayCommand(HomeCheckedCommand);
            ScheduledChecked = new RelayCommand(ScheduledCheckedCommand);
            CatalogueChecked = new RelayCommand(CatalogueCheckedCommand);
            SettingsChecked = new RelayCommand(SettingsCheckedCommand);

            CreateWindowCommand = new RelayCommand(ExecuteCreateWindowCommand);
            UpdateWindowCommand = new RelayCommand(ExecuteUpdateWindowCommand);
            DeleteWindowCommand = new RelayCommand(ExecuteDeleteWindowCommand);

            this.context = new();
            topIcon = IconChar.Home;
            sceneContainer.Content = home;
            //content = "Home";
        }
        public MainViewModel(DataContext context, UserModel model)
        {
            HomeChecked = new RelayCommand(HomeCheckedCommand);
            ScheduledChecked = new RelayCommand(ScheduledCheckedCommand);
            CatalogueChecked = new RelayCommand(CatalogueCheckedCommand);
            SettingsChecked = new RelayCommand(SettingsCheckedCommand);

            CreateWindowCommand = new RelayCommand(ExecuteCreateWindowCommand);
            UpdateWindowCommand = new RelayCommand(ExecuteUpdateWindowCommand);
            DeleteWindowCommand = new RelayCommand(ExecuteDeleteWindowCommand);

            this.context = context;
            topIcon = IconChar.Home;
            sceneContainer.Content = home;
            //content = "Home";
        }
        private void HomeCheckedCommand(object? obj)
        {
            TopIcon = IconChar.Home;
            Content = "Home";
            sceneContainer.Content = home;
        }
        private void ScheduledCheckedCommand(object? obj)
        {
            TopIcon = IconChar.Calendar;
            Content = "Scheduled";
            sceneContainer.Content = scheduled;
        }

        private void SettingsCheckedCommand(object? obj)
        {
            TopIcon = IconChar.Gear;
            Content = "Settings";
            sceneContainer.Content = settings;
        }

        private void CatalogueCheckedCommand(object? obj)
        {
            TopIcon = IconChar.Compass;
            Content = "Catalogue";
            sceneContainer.Content = catalogue;
        }

        private void ExecuteCreateWindowCommand(object? obj)
        {
            AdminCreateUserView adminCreateView = new AdminCreateUserView(context);
            adminCreateView.Show();

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteUpdateWindowCommand(object? obj)
        {
            /*AdminUpdateView adminUpdateView = new AdminUpdateView(context);
            adminUpdateView.Show();*/

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteDeleteWindowCommand(object? obj)
        {
            /*AdminDeleteView adminDeleteView = new AdminDeleteView(context);
             adminDeleteView.Show();*/

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
