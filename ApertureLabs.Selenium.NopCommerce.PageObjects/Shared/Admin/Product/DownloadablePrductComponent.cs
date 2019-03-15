using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The 'Downloadable product' component on the info tab of the admin
    /// product page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class DownloadableProductComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By downloadableProductSelector = By.CssSelector("#IsDownload");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadableProductComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public DownloadableProductComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        #endregion

        private CheckboxElement DownloadableProductElement => new CheckboxElement(
            WrappedElement.FindElement(
                downloadableProductSelector));

        #endregion

        #region Methods

        /// <summary>
        /// Gets the is downloadable product.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsDownloadableProduct()
        {
            return DownloadableProductElement.IsChecked;
        }

        /// <summary>
        /// Sets the is downloadable product.
        /// </summary>
        /// <param name="isDownload">if set to <c>true</c> [is download].</param>
        /// <returns></returns>
        public virtual DownloadableProductComponent SetIsDownloadableProduct(bool isDownload)
        {
            DownloadableProductElement.Check(isDownload);

            return this;
        }

        #endregion
    }
}
