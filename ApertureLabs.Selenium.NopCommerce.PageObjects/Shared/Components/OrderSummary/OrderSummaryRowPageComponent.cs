using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary
{
    /// <summary>
    /// OrderSummaryRowPageComponent.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class OrderSummaryRowPageComponent : PageComponent
    {
        #region Fields

        #region Selectors

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSummaryRowPageComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public OrderSummaryRowPageComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver, selector)
        { }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        public virtual int GetQuantity()
        {
            throw new NotImplementedException();
        }

        public virtual OrderSummaryRowPageComponent SetQuantity(int qty)
        {
            throw new NotImplementedException();
        }

        public virtual OrderSummaryRowPageComponent MarkForRemoval(bool remove)
        {
            throw new NotImplementedException();
        }

        public virtual IWebElement GetColumn(int columnIndex)
        {
            throw new NotImplementedException();
        }

        public virtual string GetSku()
        {
            throw new NotImplementedException();
        }

        public virtual string GetProductName()
        {
            throw new NotImplementedException();
        }

        public virtual IReadOnlyDictionary<string, string> GetAttributes()
        {
            throw new NotImplementedException();
        }

        public virtual decimal GetPrice()
        {
            throw new NotImplementedException();
        }

        public virtual decimal GetTotal()
        {
            throw new NotImplementedException();
        }

        public virtual BaseProductPage Edit()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
