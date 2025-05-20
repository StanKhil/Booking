using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Booking
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public enum MessageBoxType
    {
        ConfirmationWithYesNo = 0,
        ConfirmationWithYesNoCancel,
        Information,
        Error,
        Warning
    }
   
    public partial class CustomMessageBox : Window
    {
        private CustomMessageBox()
        {
            InitializeComponent();
        }
        static CustomMessageBox messageBox;
        static MessageBoxResult result = MessageBoxResult.No;

        public static MessageBoxResult Show(string caption, string msg, MessageBoxType type)
        {
            switch (type)
            {
                case MessageBoxType.ConfirmationWithYesNo:
                    return Show(caption, msg, MessageBoxButton.YesNo, IconChar.CircleQuestion);
                case MessageBoxType.ConfirmationWithYesNoCancel:
                    return Show(caption, msg, MessageBoxButton.YesNoCancel, IconChar.CircleQuestion);
                case MessageBoxType.Information:
                    return Show(caption, msg, MessageBoxButton.OK, IconChar.CircleInfo);
                case MessageBoxType.Error:
                    return Show(caption, msg, MessageBoxButton.OK, IconChar.ExclamationCircle);
                case MessageBoxType.Warning:
                    return Show(caption, msg, MessageBoxButton.OK, IconChar.TriangleExclamation);
                default:
                    return MessageBoxResult.No;
            }
        }
        public static MessageBoxResult Show(string msg, MessageBoxType type)
        {
            return Show(string.Empty, msg, type);
        }
        public static MessageBoxResult Show(string msg)
        {
            return Show(string.Empty, msg, MessageBoxButton.OK, IconChar.None);
        }
        public static MessageBoxResult Show(string caption, string text)
        {
            return Show(caption, text, MessageBoxButton.OK, IconChar.None);
        }
        public static MessageBoxResult Show(string caption, string text, MessageBoxButton button)
        {
            return Show(caption, text, button, IconChar.None);
        }
        public static MessageBoxResult Show(string caption, string text, MessageBoxButton button, IconChar image)
        {
            messageBox = new CustomMessageBox
            { 
                txtMsg = { Text = text }, 
                MessageTitle = { Text = caption } 
            };
            SetVisibilityOfButtons(button);
            messageBox.TopIcon.Icon = image;
            //SetImageOfMessageBox(image);

            messageBox.ShowDialog();
            return result;
        }
        private static void SetVisibilityOfButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnNo.Visibility = Visibility.Collapsed;
                    messageBox.btnYes.Visibility = Visibility.Collapsed;
                    messageBox.btnOk.Focus();
                    break;
                case MessageBoxButton.OKCancel:
                    messageBox.btnNo.Visibility = Visibility.Collapsed;
                    messageBox.btnYes.Visibility = Visibility.Collapsed;
                    messageBox.btnCancel.Focus();
                    break;
                case MessageBoxButton.YesNo:
                    messageBox.btnOk.Visibility = Visibility.Collapsed;
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnNo.Focus();
                    break;
                case MessageBoxButton.YesNoCancel:
                    messageBox.btnOk.Visibility = Visibility.Collapsed;
                    messageBox.btnCancel.Focus();
                    break;
                default:
                    break;
            }
        }
        //private static void SetImageOfMessageBox(IconChar image)
        //{
        //    switch (image)
        //    {
        //        case MessageBoxImage.Warning:
        //            messageBox.TopIcon.Icon = image;
        //            break;
        //        case MessageBoxImage.Question:
        //            messageBox.SetImage("Question.png");
        //            break;
        //        case MessageBoxImage.Information:
        //            messageBox.SetImage("Information.png");
        //            break;
        //        case MessageBoxImage.Error:
        //            messageBox.SetImage("Error.png");
        //            break;
        //        default:
        //            //messageBox.img.Visibility = Visibility.Collapsed;
        //            break;
        //    }
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnOk)
                result = MessageBoxResult.OK;
            else if (sender == btnYes)
                result = MessageBoxResult.Yes;
            else if (sender == btnNo)
                result = MessageBoxResult.No;
            else if (sender == btnCancel)
                result = MessageBoxResult.Cancel;
            else
                result = MessageBoxResult.None;
            messageBox.Close();
            messageBox = null!;
        }
        private void SetImage(string imageName)
        {
            //string uri = string.Format("/Resources/images/{0}", imageName);
            //var uriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
            //img.Source = new BitmapImage(uriSource);
        }
    }
}
