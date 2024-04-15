using Microsoft.Extensions.DependencyInjection;
using LibraryData;
using LibraryLogic;

public class Program
{
    public static void Main(string[] args)
    {
        // Create a new service collection
        var services = new ServiceCollection();
        ConfigureServices(services);

        // Build the provider
        var serviceProvider = services.BuildServiceProvider();

        // Run your application code
        var manager = serviceProvider.GetService<LibraryManager>();
        manager.AddItem(new Item { Id = 1, Title = "Sample Book", Author = "Author", PublishingYear = 2021, Publisher = "Publisher" });
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Add the LibraryContext as a singleton since we want to maintain a single instance throughout the application
        services.AddSingleton<LibraryContext>();

        // Add LibraryManager as a transient or scoped service depending on your needs
        services.AddTransient<LibraryManager>();
    }
}
