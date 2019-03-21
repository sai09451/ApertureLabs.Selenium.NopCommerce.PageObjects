using ApertureLabs.Selenium.Components.Kendo.KMultiSelect;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// The 'Prices' on the info tab of the admin product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class PricesComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By priceSelector = By.CssSelector("#Price");
        private readonly By oldPriceSelector = By.CssSelector("#OldPrice");
        private readonly By productCostSelector = By.CssSelector("#ProductCost");
        private readonly By disableBuySelector = By.CssSelector("#DisableBuyButton");
        private readonly By disableWishlistSelector = By.CssSelector("#DisableWishlistButton");
        private readonly By availableForPreOrderSelector = By.CssSelector("#AvailableForPreOrder");
        private readonly By callForPriceSelector = By.CssSelector("#AvailableForPreOrder");
        private readonly By customerEntersPriceSelector = By.CssSelector("#CustomerEntersPrice");
        private readonly By basePriceEnabledSelector = By.CssSelector("#BasepriceEnabled");
        private readonly By discountSelector = By.CssSelector("#SelectedDiscountIds");
        private readonly By taxExemptSelector = By.CssSelector("#IsTaxExempt");
        private readonly By taxCategorySelector = By.CssSelector("TaxCategoryId");
        private readonly By telecommBroadcastAndElecServicesSelector = By.CssSelector("#IsTelecommunicationsOrBroadcastingOrElectronicServices");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PricesComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public PricesComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private InputElement PriceElement => new InputElement(
            WrappedElement.FindElement(
                priceSelector));

        private InputElement OldPriceElement => new InputElement(
            WrappedElement.FindElement(
                oldPriceSelector));

        private InputElement ProductCostElement => new InputElement(
            WrappedElement.FindElement(
                productCostSelector));

        private CheckboxElement DisableBuyButtonElement => new CheckboxElement(
            WrappedElement.FindElement(
                disableBuySelector));

        private CheckboxElement DisableWishlistButtonElement => new CheckboxElement(
            WrappedDriver.FindElement(
                disableWishlistSelector));

        private CheckboxElement AvailableForPreOrderElement => new CheckboxElement(
            WrappedDriver.FindElement(
                availableForPreOrderSelector));

        private CheckboxElement CallForPriceElement => new CheckboxElement(
            WrappedElement.FindElement(
                callForPriceSelector));

        private CheckboxElement CustomerEntersPriceElement => new CheckboxElement(
            WrappedDriver.FindElement(
                customerEntersPriceSelector));

        private CheckboxElement BasePriceEnabledElement => new CheckboxElement(
            WrappedElement.FindElement(
                basePriceEnabledSelector));

        private CheckboxElement TaxExemptElement => new CheckboxElement(
            WrappedElement.FindElement(
                taxExemptSelector));

        private SelectElement TaxCategoryElement => new SelectElement(
            WrappedElement.FindElement(
                taxCategorySelector));

        private CheckboxElement TelecommBroadcastAndElecServicesElement => new CheckboxElement(
            WrappedElement.FindElement(
                telecommBroadcastAndElecServicesSelector));

        #endregion

        private KMultiSelectComponent<PricesComponent> DiscountsComponent { get; }

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
            DiscountsComponent.Load();

            return this;
        }

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetPrice()
        {
            return PriceElement.GetValue<decimal>();
        }

        /// <summary>
        /// Sets the price.
        /// </summary>
        /// <param name="price">The price.</param>
        /// <returns></returns>
        public virtual PricesComponent SetPrice(decimal price)
        {
            PriceElement.SetValue(price);

            return this;
        }

        /// <summary>
        /// Gets the old price.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetOldPrice()
        {
            return OldPriceElement.GetValue<decimal>();
        }

        /// <summary>
        /// Sets the old price.
        /// </summary>
        /// <param name="oldPrice">The old price.</param>
        /// <returns></returns>
        public virtual PricesComponent SetOldPrice(decimal oldPrice)
        {
            OldPriceElement.SetValue(oldPrice);

            return this;
        }

        /// <summary>
        /// Gets the product cost.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetProductCost()
        {
            return ProductCostElement.GetValue<decimal>();
        }

        /// <summary>
        /// Sets the product cost.
        /// </summary>
        /// <param name="productCost">The product cost.</param>
        /// <returns></returns>
        public virtual PricesComponent SetProductCost(decimal productCost)
        {
            ProductCostElement.SetValue(productCost);

            return this;
        }

        /// <summary>
        /// Gets the is buy button disabled.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsBuyButtonDisabled()
        {
            return DisableBuyButtonElement.IsChecked;
        }

        /// <summary>
        /// Sets the is buy button disabled.
        /// </summary>
        /// <param name="disable">if set to <c>true</c> [disable].</param>
        /// <returns></returns>
        public virtual PricesComponent SetIsBuyButtonDisabled(bool disable)
        {
            DisableBuyButtonElement.Check(disable);

            return this;
        }

        /// <summary>
        /// Gets the is wishlist button disabled.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsWishlistButtonDisabled()
        {
            return DisableWishlistButtonElement.IsChecked;
        }

        /// <summary>
        /// Sets the is wishlist button disabled.
        /// </summary>
        /// <param name="disable">if set to <c>true</c> [disable].</param>
        /// <returns></returns>
        public virtual PricesComponent SetIsWishlistButtonDisabled(bool disable)
        {
            DisableWishlistButtonElement.Check(disable);

            return this;
        }

        /// <summary>
        /// Gets the is available for pre order.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsAvailableForPreOrder()
        {
            return AvailableForPreOrderElement.IsChecked;
        }

        /// <summary>
        /// Sets the is available for pre order.
        /// </summary>
        /// <param name="isAvailable">if set to <c>true</c> [is available].</param>
        /// <returns></returns>
        public virtual PricesComponent SetIsAvailableForPreOrder(bool isAvailable)
        {
            AvailableForPreOrderElement.Check(isAvailable);

            return this;
        }

        /// <summary>
        /// Gets the is call for price.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsCallForPrice()
        {
            return CallForPriceElement.IsChecked;
        }

        /// <summary>
        /// Sets the is call for price.
        /// </summary>
        /// <param name="isCallForPrice">if set to <c>true</c> [is call for price].</param>
        /// <returns></returns>
        public virtual PricesComponent SetIsCallForPrice(bool isCallForPrice)
        {
            CallForPriceElement.Check(isCallForPrice);

            return this;
        }

        /// <summary>
        /// Gets the does customer enter price.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetDoesCustomerEnterPrice()
        {
            return CustomerEntersPriceElement.IsChecked;
        }

        /// <summary>
        /// Sets the does customer enter price.
        /// </summary>
        /// <param name="entersPrice">if set to <c>true</c> [enters price].</param>
        /// <returns></returns>
        public virtual PricesComponent SetDoesCustomerEnterPrice(bool entersPrice)
        {
            CustomerEntersPriceElement.Check(entersPrice);

            return this;
        }

        /// <summary>
        /// Sets the base price enabled.
        /// </summary>
        /// <param name="basePriceEnabled">if set to <c>true</c> [base price enabled].</param>
        /// <returns></returns>
        public virtual bool SetBasePriceEnabled(bool basePriceEnabled)
        {
            return BasePriceEnabledElement.IsChecked;
        }

        /// <summary>
        /// Gets the discounts.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetDiscounts()
        {
            return DiscountsComponent.GetSelectedOptions();
        }

        /// <summary>
        /// Gets all discounts.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAllDiscounts()
        {
            return DiscountsComponent.GetAllOptions();
        }

        /// <summary>
        /// Adds the discount.
        /// </summary>
        /// <param name="discountName">Name of the discount.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual PricesComponent AddDiscount(string discountName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            DiscountsComponent.SelectItem(discountName, stringComparison);

            return this;
        }

        /// <summary>
        /// Removes the discount.
        /// </summary>
        /// <param name="discountName">Name of the discount.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual PricesComponent RemoveDiscount(string discountName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            DiscountsComponent.DeselectItem(discountName, stringComparison);

            return this;
        }

        /// <summary>
        /// Gets the is tax exempt.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsTaxExempt()
        {
            return TaxExemptElement.IsChecked;
        }

        /// <summary>
        /// Sets the is tax exempt.
        /// </summary>
        /// <param name="exempt">if set to <c>true</c> [exempt].</param>
        /// <returns></returns>
        public virtual PricesComponent SetIsTaxExempt(bool exempt)
        {
            TaxExemptElement.Check(exempt);

            return this;
        }

        /// <summary>
        /// Gets the tax category.
        /// </summary>
        /// <returns></returns>
        public virtual string GetTaxCategory()
        {
            return TaxCategoryElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets all tax categories.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAllTaxCategories()
        {
            foreach (var option in TaxCategoryElement.Options)
            {
                yield return option.TextHelper().InnerText;
            }
        }

        /// <summary>
        /// Sets the tax category.
        /// </summary>
        /// <param name="taxCategory">The tax category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual PricesComponent SetTaxCategory(string taxCategory,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = TaxCategoryElement.Options.IndexOf(
                e => String.Equals(
                    e.TextHelper().InnerText,
                    taxCategory,
                    stringComparison));

            if (index == -1)
            {
                throw new NoSuchElementException();
            }

            TaxCategoryElement.SelectByIndex(index);

            return this;
        }

        /// <summary>
        /// Gets the is telecomm broadcasting and electric services.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsTelecommBroadcastingAndElectricServices()
        {
            return TelecommBroadcastAndElecServicesElement.IsChecked;
        }

        /// <summary>
        /// Gets the is telecomm broadcasting and electric services.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual PricesComponent GetIsTelecommBroadcastingAndElectricServices(bool value)
        {
            TelecommBroadcastAndElecServicesElement.Check(value);

            return this;
        }

        #endregion
    }
}
