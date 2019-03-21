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

            TierPrices = new TierPricesComponent(
                By.CssSelector("#tier-prices"),
                pageObjectFactory,
                WrappedDriver);

            Inventory = new InventoryComponent(
                By.CssSelector("#group-inventory"),
                WrappedDriver);

            Shipping = new ShippingComponent(
                By.CssSelector("#group-shipping"),
                WrappedDriver);

            Mapping = new MappingComponent(
                By.CssSelector("#group-shipping + div"),
                WrappedDriver);

            AccessControlList = new AccessControlListComponent(
                By.CssSelector("#group-shipping + div + div"),
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

        /// <summary>
        /// Gets the tier prices.
        /// </summary>
        /// <value>
        /// The tier prices.
        /// </value>
        public virtual TierPricesComponent TierPrices { get; }

        /// <summary>
        /// Gets the associated products.
        /// </summary>
        /// <value>
        /// The associated products.
        /// </value>
        public virtual AssociatedProductsVariantsComponent AssociatedProducts { get; }

        /// <summary>
        /// Gets the inventory.
        /// </summary>
        /// <value>
        /// The inventory.
        /// </value>
        public virtual InventoryComponent Inventory { get; }

        /// <summary>
        /// Gets the shipping.
        /// </summary>
        /// <value>
        /// The shipping.
        /// </value>
        public virtual ShippingComponent Shipping { get; }

        /// <summary>
        /// Gets the mapping.
        /// </summary>
        /// <value>
        /// The mapping.
        /// </value>
        public virtual MappingComponent Mapping { get; }

        /// <summary>
        /// Gets the access control list.
        /// </summary>
        /// <value>
        /// The access control list.
        /// </value>
        public virtual AccessControlListComponent AccessControlList { get; }

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
            TierPrices.Load();
            AssociatedProducts.Load();
            Inventory.Load();
            Shipping.Load();
            Mapping.Load();
            AccessControlList.Load();

            return this;
        }

        #endregion
    }
}
