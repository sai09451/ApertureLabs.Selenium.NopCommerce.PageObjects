using System;
using System.Collections.Generic;
using System.Text;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The 'General Info' tab body of the EditPage.
    /// </summary>
    public class ProductInfoComponent : FluidPageComponent<IEditPage>
    {
        #region Fields

        #region Selectors

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInfoComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="driver">The driver.</param>
        public ProductInfoComponent(By selector,
            IEditPage parent,
            IWebDriver driver)
            : base(selector, driver, parent)
        { }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        #endregion
    }
}
