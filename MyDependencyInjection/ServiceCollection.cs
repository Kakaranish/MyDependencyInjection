using System;
using System.Collections.Generic;

namespace MyDependencyInjection
{
    public class ServiceCollection
    {
        private readonly IDictionary<Type, ServiceDescriptor> _serviceDescriptors =
            new Dictionary<Type, ServiceDescriptor>();

        public ServiceContainer CreateServiceContainer()
        {
            return new(_serviceDescriptors);
        }

        public void RegisterSingleton<TService>()
        {
            EnsureServiceHasSingleCtor<TService>();
            
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton);
            AddServiceDescriptor(serviceDescriptor);
        }

        public void RegisterSingleton<TService>(TService serviceImplementation)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton)
            {
                Implementation = serviceImplementation
            };
            AddServiceDescriptor(serviceDescriptor);
        }

        public void RegisterSingleton<TService, TServiceImpl>() where TServiceImpl : TService
        {
            EnsureServiceHasSingleCtor<TServiceImpl>();

            var serviceDescriptor = new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton)
            {
                ImplementationType = typeof(TServiceImpl)
            };

            if (typeof(TServiceImpl).IsAbstract || typeof(TServiceImpl).IsInterface)
            {
                throw new ArgumentException("Service implementation cannot be abstract class or interface");
            }

            AddServiceDescriptor(serviceDescriptor);
        }

        public void RegisterSingleton<TService, TServiceImpl>(TServiceImpl serviceImpl) where TServiceImpl : TService
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton)
            {
                ImplementationType = typeof(TServiceImpl),
                Implementation = serviceImpl

            };
            AddServiceDescriptor(serviceDescriptor);
        }

        public void RegisterTransient<TService>()
        {
            EnsureServiceHasSingleCtor<TService>();
            
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient);
            AddServiceDescriptor(serviceDescriptor);
        }

        public void RegisterTransient<TService, TServiceImpl>() where TServiceImpl : TService
        {
            EnsureServiceHasSingleCtor<TServiceImpl>();
            
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient)
            {
                ImplementationType = typeof(TServiceImpl)
            };
            AddServiceDescriptor(serviceDescriptor);
        }

        private void EnsureServiceHasSingleCtor<TService>()
        {
            if (typeof(TService).GetConstructors().Length != 1)
            {
                throw new ArgumentException("Service must have exactly one constructor");
            }
        }

        private void AddServiceDescriptor(ServiceDescriptor serviceDescriptor)
        {
            if (!_serviceDescriptors.TryAdd(serviceDescriptor.ServiceType, serviceDescriptor))
            {
                throw new ArgumentException("Service is already registered");
            }
        }
    }
}