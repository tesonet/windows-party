using Caliburn.Micro;
using System;
using System.Collections.Generic;

namespace ServerLocator.Client
{
    public class Startup : BootstrapperBase
    {
		private readonly SimpleContainer container;

        protected override void Configure()
        {
			base.Configure();
        }

        public Startup()
        {
			this.container = new SimpleContainer();
		}

		protected override object GetInstance(Type serviceType, string key)
		{
			return this.container.GetInstance(serviceType, key);
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return this.container.GetAllInstances(serviceType);
		}

		protected override void BuildUp(object instance)
		{
			this.container.BuildUp(instance);
		}

	}
}
