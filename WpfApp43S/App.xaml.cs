using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Configuration;
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
        services.AddAutoMapper(
            (sp, cfg) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                cfg.LicenseKey = configuration["AutoMapperLicenseKey"];
            },
            typeof(StudentMapperProfile));
        services.AddLogging();

        services.AddSingleton(BuildConfiguration());
        services.AddSingleton<IRepository, XmlFileRepository>();

        services.AddTransient<MainWindowViewModel>();
    }

    private static IConfiguration BuildConfiguration()
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddJsonFile("appsettings.json", optional: true);

        if (!string.IsNullOrWhiteSpace(env))
        {
            configurationBuilder.AddJsonFile($"appsettings.{env}.json", optional: true);
        }

        return configurationBuilder.Build();
    }
}
