using System.Windows;
using Boss.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Boss.Desktop
{
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            DatabaseContext.Initialize();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Services serão registrados aqui no futuro
        }
    }
}