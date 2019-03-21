using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// The 'Shipping' component on the product info tab of the admin product
    /// edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class ShippingComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By shippingEnabledSelector = By.CssSelector("#IsShipEnabled");
        private readonly By weightSelector = By.CssSelector("#Weight");
        private readonly By lengthSelector = By.CssSelector("#Length");
        private readonly By widthSelector = By.CssSelector("#Width");
        private readonly By heightSelector = By.CssSelector("#Height");
        private readonly By freeShippingSelector = By.CssSelector("#IsFreeShipping");
        private readonly By shipSeperatleySelector = By.CssSelector("#ShipSeparately");
        private readonly By additionalShippingChargeSelector = By.CssSelector("#AdditionalShippingCharge");
        private readonly By deliveryDateSelector = By.CssSelector("#DeliveryDateId");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public ShippingComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement ShippingEnabledElement => new CheckboxElement(
            WrappedElement.FindElement(
                shippingEnabledSelector));

        private InputElement WeightElement => new InputElement(
            WrappedElement.FindElement(
                weightSelector));

        private InputElement LengthElement => new InputElement(
            WrappedElement.FindElement(
                lengthSelector));

        private InputElement WidthElement => new InputElement(
            WrappedElement.FindElement(
                widthSelector));

        private InputElement HeightElement => new InputElement(
            WrappedElement.FindElement(
                heightSelector));

        private CheckboxElement FreeShippingElement => new CheckboxElement(
            WrappedElement.FindElement(
                freeShippingSelector));

        private CheckboxElement ShipSeperatelyElement => new CheckboxElement(
            WrappedElement.FindElement(
                shipSeperatleySelector));

        private InputElement AdditionalShippingChargeElement => new CheckboxElement(
            WrappedElement.FindElement(
                additionalShippingChargeSelector));

        private SelectElement DeliveryDateElement => new SelectElement(
            WrappedElement.FindElement(
                deliveryDateSelector));

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the is shipping enabled.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsShippingEnabled()
        {
            return ShippingEnabledElement.IsChecked;
        }

        /// <summary>
        /// Sets the is shipping enabled.
        /// </summary>
        /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
        /// <returns></returns>
        public virtual ShippingComponent SetIsShippingEnabled(bool isEnabled)
        {
            ShippingEnabledElement.Check(isEnabled);

            return this;
        }

        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <returns></returns>
        public virtual double GetWeight()
        {
            return WeightElement.GetValue<double>();
        }

        /// <summary>
        /// Sets the weight.
        /// </summary>
        /// <param name="weight">The weight.</param>
        /// <returns></returns>
        public virtual ShippingComponent SetWeight(double weight)
        {
            WeightElement.SetValue(weight);

            return this;
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <returns></returns>
        public virtual double GetLength()
        {
            return LengthElement.GetValue<double>();
        }

        /// <summary>
        /// Sets the length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public virtual ShippingComponent SetLength(double length)
        {
            LengthElement.SetValue(length);

            return this;
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <returns></returns>
        public virtual double GetWidth()
        {
            return WidthElement.GetValue<double>();
        }

        /// <summary>
        /// Sets the width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <returns></returns>
        public virtual ShippingComponent SetWidth(double width)
        {
            WidthElement.SetValue(width);

            return this;
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <returns></returns>
        public virtual double GetHeight()
        {
            return HeightElement.GetValue<double>();
        }

        /// <summary>
        /// Sets the height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public virtual ShippingComponent SetHeight(double height)
        {
            HeightElement.SetValue(height);

            return this;
        }

        /// <summary>
        /// Gets the has free shipping.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetHasFreeShipping()
        {
            return FreeShippingElement.IsChecked;
        }

        /// <summary>
        /// Sets the has free shipping.
        /// </summary>
        /// <param name="freeShipping">if set to <c>true</c> [free shipping].</param>
        /// <returns></returns>
        public virtual ShippingComponent SetHasFreeShipping(bool freeShipping)
        {
            FreeShippingElement.Check(freeShipping);

            return this;
        }

        /// <summary>
        /// Gets the shipped seperately.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetShippedSeperately()
        {
            return ShipSeperatelyElement.IsChecked;
        }

        /// <summary>
        /// Sets the shipped seperately.
        /// </summary>
        /// <param name="shippedSeperatley">if set to <c>true</c> [shipped seperatley].</param>
        /// <returns></returns>
        public virtual ShippingComponent SetShippedSeperately(bool shippedSeperatley)
        {
            ShipSeperatelyElement.Check(shippedSeperatley);

            return this;
        }

        /// <summary>
        /// Gets the additional shipping charge.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetAdditionalShippingCharge()
        {
            return AdditionalShippingChargeElement.GetValue<decimal>();
        }

        /// <summary>
        /// Sets the additional shipping charge.
        /// </summary>
        /// <param name="charge">The charge.</param>
        /// <returns></returns>
        public virtual ShippingComponent SetAdditionalShippingCharge(double charge)
        {
            AdditionalShippingChargeElement.SetValue(charge);

            return this;
        }

        /// <summary>
        /// Gets the delivery date.
        /// </summary>
        /// <returns></returns>
        public virtual string GetDeliveryDate()
        {
            return DeliveryDateElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the available options for delivery date.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableOptionsForDeliveryDate()
        {
            foreach (var option in DeliveryDateElement.Options)
                yield return option.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the delivery date.
        /// </summary>
        /// <param name="deliveryDate">The delivery date.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual ShippingComponent SetDeliveryDate(string deliveryDate,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = DeliveryDateElement.Options.IndexOf(
                e => String.Equals(
                    e.TextHelper().InnerText,
                    deliveryDate,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            DeliveryDateElement.SelectByIndex(index);

            return this;
        }

        #endregion
    }
}
