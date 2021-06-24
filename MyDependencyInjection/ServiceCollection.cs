using System;
using System.Collections.Generic;

namespace MyDependencyInjection
{
    public class ServiceCollection
    {
        private readonly IDictionary<Type, ServiceDescriptor> _serviceDescriptors = new Dictionary<Type, ServiceDescriptor>();

        public void RegisterSingleton<T>()
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(T), ServiceLifetime.Singleton);
            if (!HasExactlyOneCtor(typeof(T)))
            {
                throw new ArgumentException("Service has multiple constructors");
            }
            
            if(!_serviceDescriptors.TryAdd(typeof(T), serviceDescriptor))
            {
                throw new ArgumentException("Service is already registered");
            }
        }

        public void RegisterSingleton<T>(T serviceImplementation)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(T), serviceImplementation, ServiceLifetime.Singleton);
            if (!_serviceDescriptors.TryAdd(typeof(T), serviceDescriptor))
            {
                throw new ArgumentException("Service is already registered");
            }
        }

        public void RegisterSingleton<TService, TServiceImpl>() where TServiceImpl : TService
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), typeof(TServiceImpl), ServiceLifetime.Singleton);
            if (!HasExactlyOneCtor(typeof(TServiceImpl)))
            {
                throw new ArgumentException("Service implementation has multiple constructors");
            }

            if (typeof(TServiceImpl).IsAbstract || typeof(TServiceImpl).IsInterface)
            {
                throw new ArgumentException("Service implementation cannot be abstract class or interface");
            }

            if (!_serviceDescriptors.TryAdd(typeof(TService), serviceDescriptor))
            {
                throw new ArgumentException("Service is already registered");
            }
        }

        public void RegisterSingleton<TService, TServiceImpl>(TServiceImpl serviceImpl) where TServiceImpl : TService
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), typeof(TServiceImpl),
                serviceImpl, ServiceLifetime.Singleton);
            if (!_serviceDescriptors.TryAdd(typeof(TService), serviceDescriptor))
            {
                throw new ArgumentException("Service is already registered");
            }
        }

        public void RegisterTransient<T>()
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(T), ServiceLifetime.Transient);

            if (!HasExactlyOneCtor(typeof(T)))
            {
                throw new ArgumentException("Service has multiple constructors");
            }

            if (!_serviceDescriptors.TryAdd(typeof(T), serviceDescriptor))
            {
                throw new ArgumentException("Service is already registered");
            }
        }

        public void RegisterTransient<TService, TServiceImpl>() where TServiceImpl : TService
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), typeof(TServiceImpl), ServiceLifetime.Transient);

            if (!HasExactlyOneCtor(typeof(TServiceImpl)))
            {
                throw new ArgumentException("Service has multiple constructors");
            }

            if (!_serviceDescriptors.TryAdd(typeof(TService), serviceDescriptor))
            {
                throw new ArgumentException("Service is already registered");
            }
        }

        public ServiceContainer CreateServiceContainer()
        {
            return new(_serviceDescriptors);
        }

        private bool HasExactlyOneCtor(Type type)
        {
            return type.GetConstructors().Length == 1;
        }
    }
}