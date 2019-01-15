using System;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.EditorSettings;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// Corresponds to the "Admin/Views/Product/Edit.cshtml" page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.BasePage" />
    public class EditPage : BasePage
    {
        #region Fields

        #region Selectors

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPage"/> class.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public EditPage(IWebDriver driver, PageSettings pageSettings)
            : base(driver, pageSettings)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public EditorSettingsComponent Settings => throw new NotImplementedException();

        #region Elements

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Backs to product list.
        /// </summary>
        /// <returns></returns>
        public virtual ListPage BackToProductList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Previews product and switches to the new window.
        /// </summary>
        /// <param name="switchToNewWindow">if set to <c>true</c> [switch to new window].</param>
        /// <returns></returns>
        public virtual string Preview(bool switchToNewWindow = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves this product and returns the list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual ListPage Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the and continue edit.
        /// </summary>
        /// <returns></returns>
        public virtual EditPage SaveAndContinueEdit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the product.
        /// TODO: Create a 'CopyProductComponent' and return that instead of
        /// the PageComponent.
        /// </summary>
        /// <returns></returns>
        public virtual PageComponent CopyProduct()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <returns></returns>
        public virtual ListPage Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Goes to tab.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="stringComparison"></param>
        protected virtual void GoToTab(string tabName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
