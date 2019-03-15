using System;
using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    /// <summary>
    /// The 'Billing and shipping' tab of the order edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.FluidPageComponent{T}" />
    public class OrderDetailsBillingShippingComponent : FluidPageComponent<IEditPage>
    {
        #region Fields

        #region Selectors

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailsBillingShippingComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public OrderDetailsBillingShippingComponent(By selector,
            IEditPage parent,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector,
                  driver,
                  parent)
        {
            this.pageObjectFactory = pageObjectFactory;

            Shipments = new KGridComponent<OrderDetailsBillingShippingComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector(""),
                pageObjectFactory,
                WrappedDriver,
                this);
        }


        #endregion

        #region Properties

        #region Elements

        #endregion

        /// <summary>
        /// Gets the shipments.
        /// </summary>
        /// <value>
        /// The shipments.
        /// </value>
        public virtual KGridComponent<OrderDetailsBillingShippingComponent> Shipments { get; }

        #endregion

        #region Methods

        /// <summary>
        /// If overloaded don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            Shipments.Load();

            return this;
        }

        /// <summary>
        /// Gets the billing address.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual AddressModel GetBillingAddress()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the shipping address.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual AddressModel GetShippingAddress()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual string GetShippingMethod()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the shipping status.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual string GetShippingStatus()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
