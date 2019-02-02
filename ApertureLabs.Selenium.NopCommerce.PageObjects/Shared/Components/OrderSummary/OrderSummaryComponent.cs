using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderTotals;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout;
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
        private readonly By cartRowSelectors = By.CssSelector("#shopping-cart-form tbody tr");
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

        public object TaxShippingInfo { get; private set; }

        public object CheckoutAttributes { get; private set; }

        public object DiscountBox { get; private set; }

        public object GiftCardBox { get; private set; }

        public object EstimateShipping { get; private set; }

        public OrderTotalsComponent OrderTotals { get; private set; }

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

        public override ILoadableComponent Load()
        {
            base.Load();



            return this;
        }

        public IList<OrderSummaryRowPageComponent> GetCartItems()
        {
            throw new NotImplementedException();
        }

        public object GetCartItem(int index)
        {
            throw new NotImplementedException();
        }

        public object GetCartItem(string productName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(string productName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        public void UpdateShoppingCart()
        {
            // Reload the page.
            Load();
            throw new NotImplementedException();
        }

        public CatalogTemplatePage ContinueShopping()
        {
            throw new NotImplementedException();
        }

        public OrderSummaryComponent AcceptTermsAndConditions(bool accept)
        {
            // Check if terms and conditions are present.
            if (WrappedDriver.FindElements(termsOfServiceSelector).Any())
                TermsOfServiceElement.Check(accept);

            return this;
        }

        public void CheckoutOpc(
            Action<IOnePageCheckoutPage> resolve,
            Action<OrderSummaryComponent> reject)
        {
            if (resolve == null)
                throw new ArgumentNullException(nameof(resolve));
            else if (reject == null)
                throw new ArgumentNullException(nameof(reject));

            CheckoutElement.Click();

            // Check if terms and conditions warning popup appears.
            var foundTermsWarning = WrappedDriver
                .Wait(
                    TimeSpan.FromSeconds(2),
                    new[] { typeof(TimeoutException) })
                .Until(d => d.FindElements(termsOfServiceSelector).Any());

            if (foundTermsWarning)
            {
                reject(this);
                return;
            }

            // Check if redirected to same page.
            if (!IsStale())
            {
                reject(this);
                return;
            }

            // Assume we've made it to the opc checkout page.
            var opcPage = pageObjectFactory.PreparePage<IOnePageCheckoutPage>();

            // Check that we are in fact on the right page.
            if (opcPage.IsStale())
                reject(this);
            else
                resolve(opcPage);
        }

        #endregion
    }
}
