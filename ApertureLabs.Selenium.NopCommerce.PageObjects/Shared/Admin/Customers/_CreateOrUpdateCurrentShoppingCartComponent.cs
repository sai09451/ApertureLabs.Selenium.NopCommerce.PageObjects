using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// Represents the _CreateOrUpdate.CurrentShoppingCart.cshtml page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class _CreateOrUpdateCurrentShoppingCartComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By shoppingCartTypeSelector = By.CssSelector("#CustomerShoppingCartSearchModel_ShoppingCartTypeId");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="_CreateOrUpdateCurrentShoppingCartComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException">pageObjectFactory</exception>
        public _CreateOrUpdateCurrentShoppingCartComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            CurrentShoppingCartGrid = new KGridComponent<_CreateOrUpdateCurrentShoppingCartComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#currentshoppingcart-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private SelectElement ShoppingCartTypeElement => new SelectElement(
            WrappedElement.FindElement(
                shoppingCartTypeSelector));

        #endregion

        private KGridComponent<_CreateOrUpdateCurrentShoppingCartComponent> CurrentShoppingCartGrid { get; }

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
            CurrentShoppingCartGrid.Load();

            return this;
        }

        /// <summary>
        /// Sets the type of the shopping cart (Defaults are 'Shopping cart' 
        /// and 'Wishlist').
        /// </summary>
        /// <param name="shoppingCartType">Type of the shopping cart.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual _CreateOrUpdateCurrentShoppingCartComponent SetShoppingCartType(
            string shoppingCartType,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var optionIndex = ShoppingCartTypeElement.Options.IndexOf(
                e => String.Equals(
                    e.TextHelper().InnerText,
                    shoppingCartType,
                    stringComparison));

            if (optionIndex == -1)
                throw new NoSuchElementException();

            ShoppingCartTypeElement.SelectByIndex(optionIndex);

            return this;
        }

        #endregion
    }
}
