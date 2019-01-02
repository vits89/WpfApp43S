﻿using System.Windows;
using WpfApp43S.Models;
using WpfApp43S.ViewModels;

namespace WpfApp43S.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((MainWindowViewModel)DataContext).SelectedStudent == null)
            {
                ((MainWindowViewModel)DataContext).SelectedStudent = new Student();
            }
        }
    }
}