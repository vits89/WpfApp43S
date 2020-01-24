using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using WpfApp43S.Infrastructure;
using WpfApp43S.Models;
using WpfApp43S.ViewModels;
using WpfApp43S.Views;

namespace WpfApp43S
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetService<MainWindowViewModel>()
            };

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddAutoMapper(typeof(StudentMapperProfile));
        }
    }
}
