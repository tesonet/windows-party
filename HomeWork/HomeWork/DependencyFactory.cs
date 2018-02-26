using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork;
using Microsoft.Practices.Unity.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace HomeWork
    {
    public static class DependencyFactory
        {
        private static IUnityContainer _container;

        public static IUnityContainer Container
            {
            get
                {
                return _container;
                }
            private set
                {
                _container = value;
                }
            }

        static DependencyFactory ()
            {
            var container = new UnityContainer ();

            var section = (UnityConfigurationSection) ConfigurationManager.GetSection ("unity");
            if ( section != null )
                {
                section.Configure (container);
                }
            container.RegisterType<ILogInPage, LogInPage> (new ContainerControlledLifetimeManager ());
            container.RegisterType<IServerListPage, ServersListPage> (new ContainerControlledLifetimeManager ());

            _container = container;
            }

        public static T Resolve<T> ()
            {
            T ret = default (T);

            if ( Container.IsRegistered (typeof (T)) )
                {
                ret = Container.Resolve<T> ();
                }

            return ret;
            }
        }
    }
