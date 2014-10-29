using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    public static class UnityCache
    {
        private static IUnityContainer _defaultContainer = null;
        private static Dictionary<string, IUnityContainer> _containers = new Dictionary<string, IUnityContainer>();

        public static IUnityContainer DefaultContainer
        {
            get
            {
                if (_defaultContainer == null)
                {
                    // only load the container once so we can cache types as they are resolved
                    _defaultContainer = new UnityContainer();
                    var section = GetUnitySection();
                    _defaultContainer.LoadConfiguration(section);
                }

                return _defaultContainer;
            }
        }

        public static IUnityContainer Container(string containerName)
        {
            IUnityContainer container;
            _containers.TryGetValue(containerName, out container);

            if (container == null)
            {
                // load container and add to dictionary 
                container = new UnityContainer();
                var section = GetUnitySection();
                container.LoadConfiguration(section, containerName);
                _containers[containerName] = container;
            }

            return container;
        }

        private static UnityConfigurationSection GetUnitySection()
        {
            var configSection = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            if (configSection == null)
            {
                throw new ConfigurationErrorsException("Unable to load unity config section");
            }
            return configSection;
        }

        public static T Resolve<T>()
        {
            return DefaultContainer.Resolve<T>();
        }

        public static T Resolve<T>(string containerName)
        {
            return Container(containerName).Resolve<T>();
        }

        public static T Resolve<T>(string containerName, string interfaceName)
        {
            return Container(containerName).Resolve<T>(interfaceName);
        }

        public static void RegisterInstance<T>(T instance)
        {
            DefaultContainer.RegisterInstance<T>(instance);
        }

        public static void RegisterInstance<T>(string containerName, T instance)
        {
            Container(containerName).RegisterInstance<T>(instance);
        }

        public static void RegisterInstance<T>(string containerName, string interfaceName, T instance)
        {
            Container(containerName).RegisterInstance<T>(interfaceName, instance);
        }
    }
}
