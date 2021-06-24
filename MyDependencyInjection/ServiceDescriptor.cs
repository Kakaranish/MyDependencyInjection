using System;
using System.ComponentModel;

namespace MyDependencyInjection
{
    public class ServiceDescriptor
    {
        private object _implementation;
        
        public Type ServiceType { get; }
        public ServiceLifetime Lifetime { get; }
        public Type ImplementationType { get; init; }
        public object Implementation
        {
            get => _implementation;
            init => _implementation = value;
        }

        public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));

            if (!Enum.IsDefined(typeof(ServiceLifetime), lifetime))
                throw new InvalidEnumArgumentException(nameof(lifetime), (int)lifetime, typeof(ServiceLifetime));
            Lifetime = lifetime;
        }

        internal void SetImplementation(object implementation)
        {
            _implementation = implementation;
        }
    }
}