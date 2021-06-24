using System;

namespace MyDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.RegisterSingleton<IGuidProvider, GuidProvider>();
            serviceCollection.RegisterSingleton<IGuidPrinter, GuidPrinter>();

            var serviceContainer = serviceCollection.CreateServiceContainer();
            var guidPrinter = serviceContainer.GetService<IGuidPrinter>();

            guidPrinter.Print();
        }
    }
}
