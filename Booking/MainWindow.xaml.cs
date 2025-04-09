using Booking.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Booking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext _context;
        public MainWindow()
        {
            _context = new DataContext();
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text += String.Join("\n", _context.UserRoles
                .Where(ur => ur.CanUpdate)
                .Include(ur => ur.UserAccesses)
                .ThenInclude(ua => ua.User)
                .Select(ur => String.Join("\n", ur.UserAccesses
                    .Select(ua => ua.User.Name)))
                );
        }
    }
}