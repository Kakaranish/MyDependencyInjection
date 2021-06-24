using System;
using System.ComponentModel;

namespace MyDependencyInjection
{
    public class ServiceDescriptor
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public object Implementation { get; internal set; }
        public ServiceLifetime Lifetime { get; }


        public ServiceDescriptor(Type serviceType, Type implementationType, object implementation, ServiceLifetime lifetime)
            : this(serviceType, implementation, lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            Implementation = implementation;
            Lifetime = lifetime;
        }
        public ServiceDescriptor(Type serviceType, Type serviceImplType, ServiceLifetime lifetime) : this(serviceType, lifetime)
        {
            ImplementationType = serviceImplType ?? throw new ArgumentNullException(nameof(serviceImplType));
        }

        public ServiceDescriptor(Type serviceType, object implementation, ServiceLifetime lifetime) : this(serviceType, lifetime)
        {
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }

        public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));

            if (!Enum.IsDefined(typeof(ServiceLifetime), lifetime))
                throw new InvalidEnumArgumentException(nameof(lifetime), (int)lifetime, typeof(ServiceLifetime));
            Lifetime = lifetime;
        }
    }
}