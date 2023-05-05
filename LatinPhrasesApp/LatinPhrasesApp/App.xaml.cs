using LatinPhrasesApp.Services;
using LatinPhrasesApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LatinPhrasesApp
{
    public partial class App : Application
    {

        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            ConfigureServices(services);

            services.AddSingleton<IDataService, DataService>(); // Register your IDataService implementation

            ServiceProvider = services.BuildServiceProvider();

            MainPage = new MainPage();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataService, DataService>();
            // Register other services and view models here...
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
