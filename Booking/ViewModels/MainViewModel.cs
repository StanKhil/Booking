using Booking.Data;
using Booking.Models;
using Booking.Views.admin;
using System.Windows.Input;
using FontAwesome.Sharp;
using System.Windows.Controls;
using Booking.Views.Scenes;
using Booking.Data.Entities;
using System.Windows;
using Booking.Views;


namespace Booking.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public DataContext context;
        UserAccess access;
        string content = "Home";
        IconChar topIcon = new();
        ContentControl sceneContainer = new();

        HomeScene home;
        Scheduled scheduled = new();
        Catalogue catalogue;
        Item item = new();
        Administrator administrator = new();
        Settings settings = new();

        private string? errorMessage = "";
        private bool isViewVisible = true;

        public UserAccess Access
        {
            get => access;
            set
            {
                access = value;
                OnPropertyChanged(nameof(Access));
            }
        }
        public Item Item
        {
            get => item;
            set
            {
                item = value;
            }
        }
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
        public ICommand ItemChecked { get; set; }
        public ICommand AdministratorChecked { get; }
        public ICommand SettingsChecked { get; }
        public ICommand Logout { get; }

        //public ICommand UserAdminWindowCommand { get; }
        //public ICommand RealtyAdminWindowCommand { get; }
     
        public MainViewModel() : this(new UserAccess())
        {
            //HomeChecked = new RelayCommand(HomeCheckedCommand);
            //ScheduledChecked = new RelayCommand(ScheduledCheckedCommand);
            //CatalogueChecked = new RelayCommand(CatalogueCheckedCommand);
            //AdministratorChecked = new RelayCommand(AdministratorCheckedCommand);
            //SettingsChecked = new RelayCommand(SettingsCheckedCommand);

            //UserAdminWindowCommand = new RelayCommand(ExecuteUserAdminWindowCommand);
            //RealtyAdminWindowCommand = new RelayCommand(ExecuteRealtyAdminWindowCommand);

            //this.context = new();
            //this.access = new();
            //topIcon = IconChar.Home;
            //home = new(this);
            //sceneContainer.Content = home;
            //content = "Home";
        }

        public MainViewModel(UserAccess access)
        {
            HomeChecked = new RelayCommand(HomeCheckedCommand);
            ScheduledChecked = new RelayCommand(ScheduledCheckedCommand);
            CatalogueChecked = new RelayCommand(CatalogueCheckedCommand);
            ItemChecked = new RelayCommand(ItemCheckedCommand);
            AdministratorChecked = new RelayCommand(AdministratorCheckedCommand);
            SettingsChecked = new RelayCommand(SettingsCheckedCommand);
            Logout = new RelayCommand(LogoutCommand);

            //UserAdminWindowCommand = new RelayCommand(ExecuteUserAdminWindowCommand);
            //RealtyAdminWindowCommand = new RelayCommand(ExecuteRealtyAdminWindowCommand);

            this.context = new();
            this.access = access;
            topIcon = IconChar.Home;
            home = new(this);
            catalogue = new(this);
            sceneContainer.Content = home;
        }

     

        public MainViewModel(DataContext context, UserModel model)
        {
            HomeChecked = new RelayCommand(HomeCheckedCommand);
            ScheduledChecked = new RelayCommand(ScheduledCheckedCommand);
            CatalogueChecked = new RelayCommand(CatalogueCheckedCommand);
            ItemChecked = new RelayCommand(ItemCheckedCommand);
            AdministratorChecked = new RelayCommand(AdministratorCheckedCommand);
            SettingsChecked = new RelayCommand(SettingsCheckedCommand);

            //UserAdminWindowCommand = new RelayCommand(ExecuteUserAdminWindowCommand);
            //RealtyAdminWindowCommand = new RelayCommand(ExecuteRealtyAdminWindowCommand);

            this.context = context;
            this.access = new();
            topIcon = IconChar.Home;
            home = new(this);
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

        private void AdministratorCheckedCommand(object? obj)
        {
            TopIcon = IconChar.FeatherAlt;
            Content = "Administrator";
            sceneContainer.Content = administrator;
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

        private void ItemCheckedCommand(object? obj)
        {
            TopIcon = IconChar.Landmark;
            Content = "Item";
            sceneContainer.Content = item;
        }

        private void LogoutCommand(object? obj)
        {
            LoginView loginView = new LoginView();
            loginView.Show();

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteUserAdminWindowCommand(object? obj)
        {
            //UserAdminView userAdminView = new UserAdminView(context);
            //userAdminView.Show();

            //IsViewVisible = false;
            //OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteRealtyAdminWindowCommand(object? obj)
        {
            RealtyAdminView realtyAdminView = new RealtyAdminView(context);
            realtyAdminView.Show();

            IsViewVisible = false;
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
