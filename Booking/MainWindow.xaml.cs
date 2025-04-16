using Booking.Data;
using Booking.Data.Entities;
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

        private void SignUp(object sender, RoutedEventArgs e)
        {
            String name = nameTextBox.Text;
            String email = emailTextBox.Text;
            String login = loginTextBox.Text;
            String pass = passTextBox.Password;

            Guid userId = Guid.NewGuid();

            User user = new()
            {
                Id = userId,
                Name = name,
                Email = email,
                RegisteredAt = DateTime.Now,
            };

            String salt = Random.Shared.Next().ToString();
            UserAccess userAccess = new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Login = login,
                Salt = salt,
                Dk = Crypto.kdf(salt, pass),
                RoleId = "SelfRegistered"
            };

            _context.Users.Add(user);
            _context.UserAccesses.Add(userAccess);

            _context.SaveChanges();

            MessageBox.Show("Registered");
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            String login = loginTextBoxSignIn.Text;
            String pass = passTextBoxSignIn.Password;
            var userAccess = _context.UserAccesses
                .Include(ua => ua.User)
                .FirstOrDefault(ua => ua.Login == login);
            if (userAccess == null)
            {
                MessageBox.Show("User not found");
                return;
            }
            String dk = Crypto.kdf(userAccess.Salt, pass);
            if (dk != userAccess.Dk)
            {
                MessageBox.Show("Wrong password");
                return;
            }
            MessageBox.Show($"Welcome {userAccess.User.Name}");
        }
    }
}