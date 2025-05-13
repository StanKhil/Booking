using Booking.Data;
using System.Windows.Controls;
using System.Windows.Input;
using Booking.Views.Scenes.Admin_Scenes;
using Booking.Data.Entities;

namespace Booking.ViewModels
{
    public class AdminViewModel : ViewModel
    {
        public DataContext context;
        public UserAccess access;
        ContentControl sceneContainer = new();

        UserScene userAdmin;
        RealtyScene realtyAdmin;
        public ContentControl SceneContainer
        {
            get => sceneContainer;
            set
            {
                sceneContainer = value;
                OnPropertyChanged(nameof(SceneContainer));
            }
        }
        public ICommand UserChecked { get; }
        public ICommand RealtiesChecked { get; }
        public AdminViewModel(UserAccess access)
        {
            UserChecked = new RelayCommand(UserCheckedCommand);
            RealtiesChecked = new RelayCommand(RealtiesCheckedCommand);

            this.context = new();
            this.access = access;
            userAdmin = new(access);
            realtyAdmin = new(access);
            sceneContainer.Content = userAdmin;
        }
        public AdminViewModel(DataContext context, UserAccess access)
        {
            this.context = context;
            sceneContainer.Content = userAdmin;
            this.access = access;
            userAdmin = new(access);
            realtyAdmin = new(access);
        }
        private void RealtiesCheckedCommand(object? obj)
        {
            sceneContainer.Content = realtyAdmin;
        }

        private void UserCheckedCommand(object? obj)
        {
            sceneContainer.Content = userAdmin;
        }

    }
}
