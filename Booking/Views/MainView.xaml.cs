﻿using Booking.Data;
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
using Booking.ViewModels;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Booking.Data.Entities;

namespace Booking.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        DataContext context;
        public MainView(DataContext context)
        {
            InitializeComponent();
            //MessageBox.Show("Another");
            this.context = context;
            MainViewModel viewModel = new();
            viewModel.OnRequestClose += (s, e) => this.Close();
            DataContext = viewModel;
        }
        public MainView(DataContext context, UserAccess access)
        {
            InitializeComponent();
            //MessageBox.Show(access.Login);
            this.context = context;
            MainViewModel viewModel = new(access);
            viewModel.OnRequestClose += (s, e) => this.Close();
            DataContext = viewModel;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void buttonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
