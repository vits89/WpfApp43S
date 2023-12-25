using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WpfApp43S.Infrastructure;
using WpfApp43S.Models;
using WpfApp43S.ViewModels;
using WpfApp43S.Views;

namespace WpfApp43S;

/// <summary>
/// Логика взаимодействия для App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        AddServices(services);

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());

        new MainWindow().Show();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StudentMapperProfile));

        services.AddSingleton<IRepository, XmlFileRepository>();

        services.AddTransient<MainWindowViewModel>();
    }
}
