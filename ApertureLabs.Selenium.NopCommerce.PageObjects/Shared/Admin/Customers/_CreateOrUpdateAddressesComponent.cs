using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// Represents the _CreateOrUpdate.Addresses.cshtml page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class _CreateOrUpdateAddressesComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By addNewAddressSelector = By.CssSelector(".btn[onclick]");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="_CreateOrUpdateAddressesComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException">pageObjectFactory</exception>
        public _CreateOrUpdateAddressesComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            AddressesGrid = new KGridComponent<_CreateOrUpdateAddressesComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#customer-addresses-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AddNewAddressElement => WrappedElement
            .FindElement(addNewAddressSelector);

        #endregion

        /// <summary>
        /// Gets the addresses grid.
        /// </summary>
        /// <value>
        /// The addresses grid.
        /// </value>
        public virtual KGridComponent<_CreateOrUpdateAddressesComponent> AddressesGrid { get; }

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
            AddressesGrid.Load();

            return this;
        }

        /// <summary>
        /// Goes to the create new address page.
        /// </summary>
        /// <returns></returns>
        public virtual IAddressCreatePage AddNewAddress()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(AddNewAddressElement, e => e.Click());

            return pageObjectFactory.PreparePage<IAddressCreatePage>();
        }

        #endregion
    }
}
