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
    public class ProductInfoComponent : PageComponent
    {
        #region Fields

        private readonly EditPage parent;

        #region Selectors

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInfoComponent"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        public ProductInfoComponent(EditPage parent,
            IWebDriver driver,
            By selector)
            : base(driver, selector)
        {
            this.parent = parent;
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        #endregion
    }
}
