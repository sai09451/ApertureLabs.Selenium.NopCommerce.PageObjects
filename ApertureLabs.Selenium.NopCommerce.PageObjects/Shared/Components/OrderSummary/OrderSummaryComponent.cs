using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.DiscountBox;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.GiftCardBox;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderTotals;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary
{
    /// <summary>
    /// OrderSummaryComponent.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class OrderSummaryComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By errorMessagesSelector = By.CssSelector(".message-error li");
        private readonly By cartRowSelectors = By.CssSelector("#shopping-cart-form .cart tbody tr");
        private readonly By continueShoppingSelector = By.CssSelector("input[name='continueshopping']");
        private readonly By updateShoppingCartSelector = By.CssSelector("input[name='updatecart']");
        private readonly By termsOfServiceSelector = By.CssSelector("#termsofservice");
        private readonly By checkoutSelector = By.CssSelector("#checkout");
        private readonly By termsOfServiceWarningSelector = By.CssSelector(".terms-of-service-warning-box");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSummaryComponent"/> class.
        /// </summary>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public OrderSummaryComponent(
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver, By.CssSelector(".order-summary-content"))
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        public virtual object TaxShippingInfo { get; private set; }

        public virtual object CheckoutAttributes { get; private set; }

        public virtual DiscountBoxComponent DiscountBox { get; private set; }

        public virtual GiftCardBoxComponent GiftCardBox { get; private set; }

        public virtual object EstimateShipping { get; private set; }

        public virtual OrderTotalsComponent OrderTotals { get; private set; }

        #region Elements

        private IReadOnlyCollection<IWebElement> ErrorMessageElements => WrappedElement.FindElements(errorMessagesSelector);
        private IReadOnlyCollection<IWebElement> CartRowElements => WrappedDriver.FindElements(cartRowSelectors);
        private IWebElement UpdateShoppintCartElement => WrappedDriver.FindElement(updateShoppingCartSelector);
        private IWebElement ContinueShoppingElement => WrappedDriver.FindElement(continueShoppingSelector);
        private CheckboxElement TermsOfServiceElement => new CheckboxElement(WrappedDriver.FindElement(termsOfServiceSelector));
        private IWebElement CheckoutElement => WrappedDriver.FindElement(checkoutSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// If overloaded don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();

            // Tax shipping info.

            // Checkout attributes.

            // Discount.

            // Giftcard.

            // Estimate shipping.

            // Order totals.

            return this;
        }

        /// <summary>
        /// Gets the cart items.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IList<OrderSummaryRowPageComponent> GetCartItems()
        {
            var rowComponents = CartRowElements
                .Select(e => pageObjectFactory.PrepareComponent(
                    new OrderSummaryRowPageComponent(
                        ByElement.FromElement(e),
                        pageObjectFactory,
                        WrappedDriver)))
                .ToList();

            return rowComponents;
        }

        /// <summary>
        /// Gets the cart item.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public virtual OrderSummaryRowPageComponent GetCartItem(int index)
        {
            var el = CartRowElements.ElementAt(index);
            var component = pageObjectFactory.PrepareComponent(
                new OrderSummaryRowPageComponent(
                    ByElement.FromElement(el),
                    pageObjectFactory,
                    WrappedDriver));

            return component;
        }

        /// <summary>
        /// Removes the product.
        /// </summary>
        /// <param name="index">The index.</param>
        public virtual void RemoveProduct(int index)
        {
            var item = GetCartItem(index);
            item.MarkForRemoval(true);

            UpdateShoppingCart();
        }

        /// <summary>
        /// Updates the shopping cart.
        /// </summary>
        public virtual void UpdateShoppingCart()
        {
            UpdateShoppintCartElement.Click();

            // Reload the page.
            Load();
        }

        /// <summary>
        /// Continues shopping.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T ContinueShopping<T>() where T : ICatalogTemplatePage
        {
            ContinueShoppingElement.Click();

            return pageObjectFactory.PreparePage<T>();
        }

        /// <summary>
        /// Accepts the terms and conditions.
        /// </summary>
        /// <param name="accept">if set to <c>true</c> [accept].</param>
        /// <returns></returns>
        public virtual OrderSummaryComponent AcceptTermsAndConditions(bool accept)
        {
            // Check if terms and conditions are present.
            if (WrappedDriver.FindElements(termsOfServiceSelector).Any())
                TermsOfServiceElement.Check(accept);

            return this;
        }

        /// <summary>
        /// Checkouts the opc.
        /// </summary>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        /// <exception cref="ArgumentNullException">
        /// resolve
        /// or
        /// reject
        /// </exception>
        public virtual void CheckoutOpc(
            Action<ICheckoutStepPage> resolve,
            Action<OrderSummaryComponent> reject)
        {
            if (resolve == null)
                throw new ArgumentNullException(nameof(resolve));
            else if (reject == null)
                throw new ArgumentNullException(nameof(reject));

            if (!TryToProceedToCheckout())
                reject(this);

            // TODO: Check if we're on the 'Confirm guest checkout page'.

            // Assume we've made it to the opc checkout page.
            var opcPage = pageObjectFactory.PreparePage<ICheckoutStepPage>();

            // Check that we are in fact on the right page.
            if (opcPage.IsStale())
                reject(this);
            else
                resolve(opcPage);
        }

        /// <summary>
        /// Attempts to perform the full checkout process.
        /// </summary>
        public virtual IOrderDetailsPage FullCheckout()
        {
            if (!TryToProceedToCheckout())
                throw new Exception("Unable to proceed to the checkout page.");

            var checkoutPage = pageObjectFactory.PreparePage<ICheckoutPage>();

            throw new NotImplementedException();
        }

        /// <summary>
        /// Proceeds to a custom checkout page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T ProceedToCheckout<T>() where T : ICheckoutPage
        {
            if (!TryToProceedToCheckout())
                throw new Exception("Unable to proceed to the checkout page.");

            return pageObjectFactory.PreparePage<T>();
        }

        /// <summary>
        /// Proceeds to checkout.
        /// </summary>
        /// <returns></returns>
        public virtual ICheckoutPage ProceedToCheckout()
        {
            if (!TryToProceedToCheckout())
                throw new Exception("Unable to proceed to the checkout page.");

            return ProceedToCheckout<ICheckoutPage>();
        }

        private bool TryToProceedToCheckout()
        {
            CheckoutElement.Click();

            // Check if terms and conditions warning popup appears.
            var foundTermsWarning = WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .Exists(termsOfServiceSelector);

            if (foundTermsWarning)
                return false;

            // Check if redirected to same page.
            if (!IsStale())
                return false;

            return true;
        }

        #endregion
    }
}
