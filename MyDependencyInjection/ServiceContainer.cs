using System;
using System.Collections.Generic;

namespace MyDependencyInjection
{
    public class ServiceContainer
    {
        private readonly IDictionary<Type, ServiceDescriptor> _serviceDescriptors;

        internal ServiceContainer(IDictionary<Type, ServiceDescriptor> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors ?? throw new ArgumentNullException(nameof(serviceDescriptors));
        }

        public T GetService<T>()
        {
            if (!_serviceDescriptors.TryGetValue(typeof(T), out var serviceDescriptor))
            {
                throw new ArgumentException($"There is no service of type {typeof(T)} registered");
            }

            if (serviceDescriptor.Lifetime == ServiceLifetime.Singleton)
            {
                if (serviceDescriptor.Implementation == null)
                {
                    var typeToInstantiate = serviceDescriptor.ImplementationType ?? serviceDescriptor.ServiceType;
                    var instance = Activator.CreateInstance(typeToInstantiate);
                    serviceDescriptor.Implementation = instance;
                }

                return (T) serviceDescriptor.Implementation;
            }
            if (serviceDescriptor.Lifetime == ServiceLifetime.Transient)
            {
                var instance = Activator.CreateInstance<T>();
                return instance;
            }

            throw new NotImplementedException();
        }
    }
}