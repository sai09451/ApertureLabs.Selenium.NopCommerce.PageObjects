using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// Correlates to the Admin/Views/Product/_CreateOrUpdate.Info.cshtml view.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class _CreateOrUpdateInfoComponent : PageComponent
    {
        #region Fields

        #region Selectors

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="_CreateOrUpdateInfoComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException">pageObjectFactory</exception>
        public _CreateOrUpdateInfoComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            GeneralInformation = new GeneralInformationComponent(
                By.CssSelector(".panel.panel-default"),
                pageObjectFactory,
                WrappedDriver);

            GiftCard = new GroupGiftCardComponent(
                By.CssSelector("#group-giftcard"),
                WrappedDriver);

            DownloadableProduct = new DownloadableProductComponent(
                By.CssSelector("#group-downloads"),
                WrappedDriver);

            RecurringProduct = new RecurringProductComponent(
                By.CssSelector("#group-recurring"),
                WrappedDriver);

            Rental = new RentalComponent(
                By.CssSelector("#group-rental"),
                WrappedDriver);

            Prices = new PricesComponent(
                By.CssSelector("#group-prices"),
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

        /// <summary>
        /// Gets the general information.
        /// </summary>
        /// <value>
        /// The general information.
        /// </value>
        public virtual GeneralInformationComponent GeneralInformation { get; }

        /// <summary>
        /// Gets the gift card.
        /// </summary>
        /// <value>
        /// The gift card.
        /// </value>
        public virtual GroupGiftCardComponent GiftCard { get; }

        /// <summary>
        /// Gets the downloadable product.
        /// </summary>
        /// <value>
        /// The downloadable product.
        /// </value>
        public virtual DownloadableProductComponent DownloadableProduct { get; }

        /// <summary>
        /// Gets the recurring product.
        /// </summary>
        /// <value>
        /// The recurring product.
        /// </value>
        public virtual RecurringProductComponent RecurringProduct { get; }

        /// <summary>
        /// Gets the rental.
        /// </summary>
        /// <value>
        /// The rental.
        /// </value>
        public virtual RentalComponent Rental { get; }

        /// <summary>
        /// Gets the prices.
        /// </summary>
        /// <value>
        /// The prices.
        /// </value>
        public virtual PricesComponent Prices { get; }

        #endregion

        #region Methods

        /// <summary>
        /// If overriding don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            GeneralInformation.Load();
            GiftCard.Load();
            DownloadableProduct.Load();
            RecurringProduct.Load();
            Rental.Load();
            Prices.Load();

            return this;
        }

        #endregion
    }
}
