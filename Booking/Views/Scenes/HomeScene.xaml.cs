﻿using Booking.Data.Entities;
using Booking.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Booking.Views.Scenes
{
    /// <summary>
    /// Interaction logic for HomeScene.xaml
    /// </summary>
    public partial class HomeScene : UserControl
    {
        public HomeScene(MainViewModel mainViewModel, UserAccess access)
        {
            InitializeComponent();
            this.DataContext = new HomeViewModel(mainViewModel, access);
        }
    }
}
