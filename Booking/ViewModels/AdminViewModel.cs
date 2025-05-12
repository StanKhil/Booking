using Booking.Data;
using System.Windows.Controls;
using System.Windows.Input;
using Booking.Views.Scenes.Admin_Scenes;

namespace Booking.ViewModels
{
    public class AdminViewModel : ViewModel
    {
        public DataContext context;
        ContentControl sceneContainer = new();

        UserScene userAdmin = new();
        RealtyScene realtyAdmin = new();
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
        public AdminViewModel()
        {
            UserChecked = new RelayCommand(UserCheckedCommand);
            RealtiesChecked = new RelayCommand(RealtiesCheckedCommand);

            this.context = new();
            sceneContainer.Content = userAdmin;
        }
        public AdminViewModel(DataContext context)
        {
            this.context = context;
            sceneContainer.Content = userAdmin;
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
