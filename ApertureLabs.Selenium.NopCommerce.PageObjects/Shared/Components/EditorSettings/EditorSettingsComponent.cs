using System;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.EditorSettings
{
    /// <summary>
    /// The 'Settings' button available on most editor pages which will
    /// determine which fields will be displayed/hidden when the view is
    /// switched between 'Basic' and 'Advanced'.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class EditorSettingsComponent : PageComponent
    {
        #region Fields

        private readonly EditorSettingsComponentConfiguration configuration;

        #region Selectors

        private readonly By closeModalSelector = By.CssSelector(".close");

        private By modalSelector;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorSettingsComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="configuration"></param>
        public EditorSettingsComponent(By selector,
            IWebDriver driver,
            EditorSettingsComponentConfiguration configuration)
            : base(driver, selector)
        {
            this.configuration = configuration;
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement ModalElement => WrappedDriver.FindElement(modalSelector);
        private IWebElement CloseModalElement => ModalElement.FindElement(closeModalSelector);

        #endregion

        #endregion

        #region Methods

        /// <inheritDoc/>
        public override ILoadableComponent Load()
        {
            base.Load();

            var dataTarget = WrappedElement.GetAttribute("data-target");
            modalSelector = By.CssSelector(dataTarget);

            return this;
        }

        /// <summary>
        /// Gets the mode ("Basic" or "Advanced").
        /// </summary>
        /// <returns>Basic or Advanced.</returns>
        public string GetMode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public EditorSettingsComponent SetMode(string mode,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        private bool IsOpen()
        {
            return ModalElement.Displayed;
        }

        private void Open()
        {
            if (!IsOpen())
            {
                WrappedElement.Click();
            }
        }

        private void Close()
        {
            if (IsOpen())
            {
                if (configuration.UseKeyboardInsteadOfMouseToInteract)
                    ModalElement.SendKeys(Keys.Escape);
                else
                    CloseModalElement.Click();
            }
        }

        #endregion
    }
}
