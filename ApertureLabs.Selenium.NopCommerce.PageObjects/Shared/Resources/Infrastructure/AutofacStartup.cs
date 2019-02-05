using System;
using System.Linq;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Autofac;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Infrastructure
{
    /// <summary>
    /// Creates an Autofac container.
    /// </summary>
    public class AutofacStartup : Module, IOrderedModule
    {
        /// <summary>
        /// Order is zero.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order => 0;

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
            // Shouldn't register PageSettings here.
            //builder.RegisterInstance(new PageSettings());

            // Register all payment method handlers here.
            var paymentMethodHandlerTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsAssignableTo<Public.Checkout.IPaymentMethodHandler>()
                    && !t.IsAbstract
                    && t.IsClass
                    && t.IsVisible
                    && t.IsPublic)
                .ToArray();

            foreach (var type in paymentMethodHandlerTypes)
            {
                builder.RegisterTypes(type)
                    .As<Public.Checkout.IPaymentMethodHandler>()
                    .FindConstructorsWith(t => t.GetConstructors())
                    .InstancePerLifetimeScope();
            }

            #region Admin Pages

            builder
                .RegisterType<Admin.BasePage>()
                .As<Admin.IBasePage>()
                .SingleInstance();
            builder
                .RegisterType<Admin.Home.HomePage>()
                .As<Admin.Home.IHomePage>()
                .SingleInstance();
            builder
                .RegisterType<Admin.Product.ListPage>()
                .As<Admin.Product.IListPage>()
                .SingleInstance();
            builder
                .RegisterType<Admin.Product.EditPage>()
                .As<Admin.Product.IEditPage>()
                .SingleInstance();

            #endregion

            #region Public Pages

            builder
                .RegisterType<Public.BasePage>()
                .As<Public.IBasePage>()
                .SingleInstance();
            builder.RegisterType<Public.Catalog.ParentCategoryPage>()
                .As<Public.Catalog.IParentCategoryPage>()
                .SingleInstance();
            builder
                .RegisterType<Public.Catalog.ProductTagsAllPage>()
                .As<Public.Catalog.IProductTagsAllPage>()
                .SingleInstance();
            builder
                .RegisterType<Public.Catalog.ManufacturerAllPage>()
                .As<Public.Catalog.IManufacturerAllPage>()
                .SingleInstance();
            builder
                .RegisterType<Public.Catalog.ProductsByManufacturerPage>()
                .As<Public.Catalog.IProductsByManufacturerPage>()
                .SingleInstance();
            builder
                .RegisterType<Public.Catalog.ProductsByCategoryPage>()
                .As<Public.Catalog.IProductsByCategoryPage>()
                .SingleInstance();
            builder
                .RegisterType<Public.Catalog.ProductsByTagPage>()
                .As<Public.Catalog.IProductsByTagPage>()
                .SingleInstance();
            builder
                .RegisterType<Public.Catalog.SearchPage>()
                .As<Public.Catalog.ISearchPage>()
                .SingleInstance();
            builder
                .RegisterType<Public.Customer.LoginPage>()
                .As<Public.Customer.ILoginPage>()
                .SingleInstance();
            builder
                .RegisterType<Public.Home.HomePage>()
                .As<Public.Home.IHomePage>()
                .SingleInstance();

            // TODO: Replace this with the different product pages.
            builder
                .RegisterType<Public.Product.BaseProductPage>()
                .As<Public.Product.IBaseProductPage>()
                .SingleInstance();

            builder
                .RegisterType<Public.ShoppingCart.CartPage>()
                .As<Public.ShoppingCart.ICartPage>()
                .SingleInstance();
            builder.RegisterType<Public.Checkout.OnePageCheckoutPage>()
                .As<Public.Checkout.ICheckoutStepPage>()
                .As<Public.Checkout.ICheckoutPage>()
                .SingleInstance();

            #endregion

            base.Load(builder);
        }
    }
}
