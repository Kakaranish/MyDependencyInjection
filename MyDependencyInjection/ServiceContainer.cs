using System;
using System.Collections.Generic;
using System.Linq;

namespace MyDependencyInjection
{
    public class ServiceContainer
    {
        private readonly IDictionary<Type, ServiceDescriptor> _serviceDescriptors;

        internal ServiceContainer(IDictionary<Type, ServiceDescriptor> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors ?? throw new ArgumentNullException(nameof(serviceDescriptors));
        }

        public object GetService(Type serviceType)
        {
            if (!_serviceDescriptors.TryGetValue(serviceType, out var serviceDescriptor))
            {
                throw new ArgumentException($"There is no service of type {serviceType.Name} registered");
            }

            if (serviceDescriptor.Lifetime == ServiceLifetime.Transient)
            {
                return InstantiateService(serviceDescriptor);
            }

            return serviceDescriptor.Implementation ?? (serviceDescriptor.Implementation = InstantiateService(serviceDescriptor));
        }

        private object InstantiateService(ServiceDescriptor serviceDescriptor)
        {
            var typeToInstantiate = serviceDescriptor.ImplementationType ?? serviceDescriptor.ServiceType;

            var ctor = typeToInstantiate.GetConstructors().SingleOrDefault();
            var ctorArgs = ctor.GetParameters().Select(x => GetService(x.ParameterType)).ToArray();

            var instance = Activator.CreateInstance(typeToInstantiate, ctorArgs);

            return instance;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }
    }
}