using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
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

        private readonly IPageObjectFactory pageObjectFactory;
        private readonly EditorSettingsComponent settings;

        #region Selectors

        private readonly By backToProductListSelector = By.CssSelector("#product-form > div.content-header.clearfix > h1 > small > a");
        private readonly By advancedSwitchSelector = By.CssSelector(".onoffswitch");
        private readonly By settingsBySelector = By.CssSelector("#product-editor-settings");
        private readonly By previewButtonSelector = By.CssSelector("#product-form > div.content-header.clearfix > div > button.btn.bg-purple");
        private readonly By saveButtonSelector = By.CssSelector("#product-form > div.content-header.clearfix > div > button:nth-child(2)");
        private readonly By saveAndContinueEditButtonSelector = By.CssSelector("#product-form > div.content-header.clearfix > div > button:nth-child(3)");
        private readonly By copyProductButtonSelector = By.CssSelector("#product-form > div.content-header.clearfix > div > button.btn.bg-olive");
        private readonly By deleteButtonSelector = By.CssSelector("#product-delete");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPage"/> class.
        /// </summary>
        /// <param name="pageObjectFactory"></param>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public EditPage(IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver, pageSettings)
        {
            this.pageObjectFactory = pageObjectFactory;

            settings = new EditorSettingsComponent(advancedSwitchSelector,
                settingsBySelector,
                WrappedDriver,
                EditorSettingsComponentConfiguration.DefaultConfiguration());
        }

        #endregion

        #region Properties

        #region Elements

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public EditorSettingsComponent Settings => pageObjectFactory.PrepareComponent(settings);

        private IWebElement BackToProductListElement => WrappedDriver.FindElement(backToProductListSelector);
        private IWebElement PreviewButtonElement => WrappedDriver.FindElement(previewButtonSelector);
        private IWebElement SaveButtonElement => WrappedDriver.FindElement(saveButtonSelector);
        private IWebElement SaveAndContinueButtonElement => WrappedDriver.FindElement(saveAndContinueEditButtonSelector);
        private IWebElement CopyProductButtonElement => WrappedDriver.FindElement(copyProductButtonSelector);
        private IWebElement DeleteButtonElement => WrappedDriver.FindElement(deleteButtonSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Backs to product list.
        /// </summary>
        /// <returns></returns>
        public virtual ListPage BackToProductList()
        {
            BackToProductListElement.Click();

            return pageObjectFactory.PreparePage<ListPage>();
        }

        /// <summary>
        /// Previews product and switches to the new window.
        /// </summary>
        /// <param name="switchToNewWindow">if set to <c>true</c> [switch to new window].</param>
        /// <returns></returns>
        public virtual string Preview(bool switchToNewWindow = true)
        {
            var oldWindowHandles = WrappedDriver.WindowHandles;
            PreviewButtonElement.Click();

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => d.WindowHandles.Count != oldWindowHandles.Count);

            var newHandle = WrappedDriver.WindowHandles
                .Except(oldWindowHandles)
                .First();

            if (switchToNewWindow)
                WrappedDriver.SwitchTo().Window(newHandle);

            return newHandle;
        }

        /// <summary>
        /// Saves this product and returns the list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual ListPage Save()
        {
            SaveButtonElement.Click();

            return pageObjectFactory.PreparePage<ListPage>();
        }

        /// <summary>
        /// Saves the and continue edit.
        /// </summary>
        /// <returns></returns>
        public virtual EditPage SaveAndContinueEdit()
        {
            SaveAndContinueButtonElement.Click();

            return pageObjectFactory.PreparePage<EditPage>();
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
            DeleteButtonElement.Click();

            return pageObjectFactory.PreparePage<ListPage>();
        }

        /// <summary>
        /// Goes to tab.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="stringComparison"></param>
        public virtual void GoToTab(string tabName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the tab names.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IEnumerable<string> GetTabNames()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the name of the active tab.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual string GetActiveTabName()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
