using System;

namespace MyDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            //var guidProvider = new GuidProvider();
            serviceCollection.RegisterSingleton<IGuidProvider>(new GuidProvider());
            
            var serviceContainer = serviceCollection.CreateServiceContainer();

            var provider1 = serviceContainer.GetService<IGuidProvider>();
            var provider2 = serviceContainer.GetService<IGuidProvider>();

            Console.WriteLine(provider1.GetGuid());
            Console.WriteLine(provider2.GetGuid());
        }
    }
}
