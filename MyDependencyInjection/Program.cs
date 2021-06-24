using System;

namespace MyDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.RegisterTransient<IGuidProvider, GuidProvider>();
            serviceCollection.RegisterSingleton<IGuidPrinter, GuidPrinter>();

            var serviceContainer = serviceCollection.CreateServiceContainer();
            var guidPrinter1 = serviceContainer.GetService<IGuidPrinter>();
            var guidPrinter2 = serviceContainer.GetService<IGuidPrinter>();

            guidPrinter1.Print();
            guidPrinter2.Print();
        }
    }
}
