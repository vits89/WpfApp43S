using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using WpfApp43S.ViewModels;

namespace WpfApp43S.Views;

/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
    }
}
