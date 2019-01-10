using System;
using System.Collections.Generic;
using System.Text;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Factories;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using Autofac;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Infrastructure
{
    /// <summary>
    /// Creates an Autofac container.
    /// </summary>
    public class AutofacStartup : Module
    {
        /// <summary>
        /// Initializes an autofac container.
        /// </summary>
        public void Startup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CustomPageObjectFactory>()
                .As<IPageObjectFactory>();

            builder.RegisterInstance(new PageSettings());

            var container = builder.Build();
        }

        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
