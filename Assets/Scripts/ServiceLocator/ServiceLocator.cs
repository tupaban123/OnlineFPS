using System;
using System.Collections.Generic;

namespace OnlineFPS.CodeBase
{
    public class ServiceLocator
    {
        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();

        public void Register<T>(IService service) where T : IService
        {
            var key = typeof(T);

            if (_services.ContainsKey(key))
                throw new Exception($"Duplicate service [{typeof(T)}]");

            _services.Add(key, service);
        }

        public void Unregister<T>() where T : IService
        {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
                throw new Exception($"Can't unregister service [{typeof(T)}]");

            _services.Remove(key);
        }

        public T Get<T>() where T : IService
        {
            var key = typeof(T);

            if (!_services.ContainsKey(key))
                throw new Exception($"Can't find service [{typeof(T)}]");

            return (T)_services[key];
        }
    }
}