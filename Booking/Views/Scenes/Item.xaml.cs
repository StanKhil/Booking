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
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        private const int height = 450;
        public Item()
        {
            InitializeComponent();
            itemScrollViewer.Height = 0;
            ItemViewModel itemViewModel = new();
            DataContext = itemViewModel;
        }
        public Item(Realty realty, UserAccess access)
        {
            InitializeComponent();
            itemScrollViewer.Height = height;
            ItemViewModel itemViewModel = new(realty, access);
            DataContext = itemViewModel;
        }

        private async void Item_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ItemViewModel viewModel)
            {
                await viewModel.Window_LoadedAsync();
            }
        }        
    }
}
