using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KDatePicker;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// Represents the 'Tier prices' component of the info tab of the admin
    /// product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class TierPricesComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By addNewTierPriceSelector = By.CssSelector("#btnAddNewTierPrice");
        private readonly By quantitySelector = By.CssSelector("#Quantity");
        private readonly By priceSelector = By.CssSelector("#Price");
        private readonly By storeSelector = By.CssSelector("#StoreId");
        private readonly By customerRoleSelector = By.CssSelector("#CustomerRoleId");
        private readonly By startDateSelector = By.CssSelector("#StartDateTimeUtc");
        private readonly By endDateSelector = By.CssSelector("#EndDateTimeUtc");
        private readonly By saveSelector = By.CssSelector("*[name='save']");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TierPricesComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public TierPricesComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            TierPricesGrid = new KGridComponent<TierPricesComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#tierprices-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);

            StartDateComponent = new KDatePickerComponent<TierPricesComponent>(
                new KDatePickerConfiguration(),
                startDateSelector,
                WrappedDriver,
                this);

            EndDateComponent = new KDatePickerComponent<TierPricesComponent>(
                new KDatePickerConfiguration(),
                endDateSelector,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement SaveElement => WrappedDriver
            .FindElement(saveSelector);

        // Using WrappedDriver instead of WrappedElement as this element
        // appears in a new window.
        private InputElement QuantityElement => new InputElement(
            WrappedDriver.FindElement(
                quantitySelector));

        // Using WrappedDriver instead of WrappedElement as this element
        // appears in a new window.
        private InputElement PriceElement => new InputElement(
            WrappedDriver.FindElement(
                priceSelector));

        // Using WrappedDriver instead of WrappedElement as this element
        // appears in a new window.
        private SelectElement StoreElement => new SelectElement(
            WrappedDriver.FindElement(
                storeSelector));

        // Using WrappedDriver instead of WrappedElement as this element
        // appears in a new window.
        private SelectElement CustomerRoleElement => new SelectElement(
            WrappedDriver.FindElement(
                customerRoleSelector));

        private IWebElement AddNewTierPriceElement => WrappedElement
            .FindElement(addNewTierPriceSelector);

        #endregion

        /// <summary>
        /// Gets the start date component. Don't load in <see cref="Load"/>!.
        /// </summary>
        /// <value>
        /// The start date component.
        /// </value>
        private KDatePickerComponent<TierPricesComponent> StartDateComponent { get; }

        /// <summary>
        /// Gets the end date component. Don't load in <see cref="Load"/>!.
        /// </summary>
        /// <value>
        /// The end date component.
        /// </value>
        private KDatePickerComponent<TierPricesComponent> EndDateComponent { get; }

        /// <summary>
        /// Gets the tier prices grid.
        /// </summary>
        /// <value>
        /// The tier prices grid.
        /// </value>
        public virtual KGridComponent<TierPricesComponent> TierPricesGrid { get; }

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
            TierPricesGrid.Load();

            return this;
        }

        /// <summary>
        /// Adds the new tier price.
        /// </summary>
        /// <param name="tierPricingModel">The tier pricing model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">tierPricingModel</exception>
        public virtual TierPricesComponent AddNewTierPrice(
            TierPriceModel tierPricingModel)
        {
            if (tierPricingModel == null)
                throw new ArgumentNullException(nameof(tierPricingModel));

            var cachedWindowHandle = WrappedDriver.CurrentWindowHandle;
            var cachedWindowHandles = WrappedDriver.WindowHandles;
            AddNewTierPriceElement.Click();

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => d.WindowHandles.Count > cachedWindowHandles.Count);

            // Switch to new window.
            var newHandle = WrappedDriver.WindowHandles
                .Except(cachedWindowHandles)
                .First();

            WrappedDriver.SwitchTo().Frame(newHandle);

            // Load the StartDate/EndDate components.
            StartDateComponent.Load();
            EndDateComponent.Load();

            // Enter values.
            QuantityElement.SetValue(tierPricingModel.Quantity);
            PriceElement.SetValue(tierPricingModel.Price);
            StoreElement.SelectByText(tierPricingModel.Store);
            CustomerRoleElement.SelectByText(tierPricingModel.CustomerRole);
            StartDateComponent.SetValue(tierPricingModel.StartDate);
            EndDateComponent.SetValue(tierPricingModel.EndDate);

            // Save.
            var saveEl = SaveElement; // Cache this to avoid extra lookups.
            saveEl.Click();
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => saveEl.IsStale());

            // Switch back to the original handle.
            WrappedDriver.SwitchTo().Frame(cachedWindowHandle);

            return this;
        }

        /// <summary>
        /// Gets the listed tier prices.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TierPriceModel> GetListedTierPrices()
        {
            var enCulture = CultureInfo.GetCultureInfo("en-US");
            var totalRows = TierPricesGrid.GetNumberOfRows();

            for (var rowIndex = 0; rowIndex < totalRows; rowIndex++)
            {
                var storeCell = TierPricesGrid.GetCell(rowIndex, 0);
                var customerRoleCell = TierPricesGrid.GetCell(rowIndex, 1);
                var quantityCell = TierPricesGrid.GetCell(rowIndex, 2);
                var priceCell = TierPricesGrid.GetCell(rowIndex, 3);
                var startDateCell = TierPricesGrid.GetCell(rowIndex, 4);
                var endDateCell = TierPricesGrid.GetCell(rowIndex, 5);

                var model = new TierPriceModel
                {
                    Store = storeCell.TextHelper().InnerText,
                    CustomerRole = customerRoleCell.TextHelper().InnerText,
                    Quantity = quantityCell.TextHelper().ExtractInteger(),
                    Price = priceCell.TextHelper().ExtractPrice()
                };

                var startDateTxt = startDateCell.TextHelper().InnerText;
                var endDateTxt = endDateCell.TextHelper().InnerText;

                if (!String.IsNullOrEmpty(startDateTxt))
                {
                    var startDate = DateTime.ParseExact(
                        startDateTxt,
                        "M/d/yyyy h/mm/ss tt",
                        enCulture);

                    model.StartDate = startDate;
                }

                if (!String.IsNullOrEmpty(endDateTxt))
                {
                    var endDate = DateTime.ParseExact(
                        endDateTxt,
                        "M/d/yyyy h/mm/ss tt",
                        enCulture);

                    model.EndDate = endDate;
                }

                yield return model;
            }
        }

        #endregion
    }
}
