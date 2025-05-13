using Booking.Data.Entities;
using Booking.ViewModels;
using Booking.ViewModels.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Booking.Views.Scenes.Admin_Scenes
{
    /// <summary>
    /// Interaction logic for RealtyScene.xaml
    /// </summary>
    public partial class RealtyScene : System.Windows.Controls.UserControl
    {
        private UserAccess access;

        public RealtyScene(UserAccess access)
        {
            InitializeComponent();
            this.access = access;
            this.DataContext = new RealtyAdminViewModel(access);
        }
        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "All files (*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = openFileDialog.FileName;
                System.Windows.Forms.MessageBox.Show(selectedPath);
                SelectedFileText.Text = selectedPath;
            }
        }

    }
    
}
