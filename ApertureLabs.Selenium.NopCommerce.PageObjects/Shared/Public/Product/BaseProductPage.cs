using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.ProductBreadCrumb;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Factories;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using ApertureLabs.Selenium;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Meta;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product
{
    /// <summary>
    /// Base class for the product pages.
    /// </summary>
    public partial class BaseProductPage : BasePage, IViewModel<ProductDetailsModel>
    {
        #region Fields

        #region Selectors

        private readonly By addToCartSelector = By.CssSelector(".add-to-cart");
        private readonly By addToCartCustomSelector = By.CssSelector(".add-to-cart-custom");
        private readonly By paymentPlanSelector = By.CssSelector("#paymentPlan");
        private readonly By fullDescriptionSelector = By.CssSelector(".full-description");
        private readonly By shortDescriptionSelector = By.CssSelector(".short-description");
        private readonly By installmentPlansSelector = By.CssSelector(".pdpPaymentInstallmentsTable");
        private readonly By subscriptionPlanSelector = By.CssSelector("#subscriptionOption");
        private readonly By subscriptionDetailsSelector = By.CssSelector(".subscriptionOptionWrap");
        private readonly By openRemodalSelector = By.CssSelector(".remodal-wrapper.remodal-is-opened");
        private readonly By productNameSelector = By.CssSelector(".product-name *[itemprop='name']");
        private readonly By metaDescriptionSelector = By.CssSelector("meta[name='description']");
        private readonly By metaKeywordsSelector = By.CssSelector("meta[name='keywords']");
        private readonly By productFormSelector = By.CssSelector(".page-body > form");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="settings"></param>
        public BaseProductPage(IWebDriver driver, PageSettings settings)
            : base(driver, settings)
        {
            pageObjectFactory = new CustomPageObjectFactory();
        }

        #endregion

        #region Properties

        #region Elements

        /// <summary>
        /// Uses the default add-to-cart button if visible, else uses the
        /// custom add-to-cart button.
        /// </summary>
        private IWebElement AddToCartButtonElement
        {
            get
            {
                // The default add to cart selector.
                var el = WrappedDriver.FindElement(addToCartSelector);

                // If it's not displayed use the custom add to cart element.
                if (el.Displayed == false)
                {
                    el = WrappedDriver.FindElement(addToCartCustomSelector);
                }

                return el;
            }
        }

        private SelectElement PaymentPlanSelectElement => new SelectElement(WrappedDriver.FindElement(paymentPlanSelector));
        private IList<IWebElement> InstallmentPlanElements => WrappedDriver.FindElements(installmentPlansSelector);
        private SelectElement SubscriptionPlanElement => new SelectElement(WrappedDriver.FindElement(subscriptionPlanSelector));
        private IList<IWebElement> SubscriptionDetailElements => WrappedDriver.FindElements(subscriptionDetailsSelector);
        private IWebElement OpenedRemodalElement => WrappedDriver.FindElement(openRemodalSelector);
        private IWebElement ProductNameElement => WrappedDriver.FindElement(productNameSelector);
        private IWebElement FullDescriptionElement => WrappedDriver.FindElement(fullDescriptionSelector);
        private IWebElement ShortDescriptionElement => WrappedDriver.FindElement(shortDescriptionSelector);
        private MetaElement MetaDescriptionElement => new MetaElement(WrappedDriver.FindElement(metaDescriptionSelector));
        private MetaElement MetaKeywordsElement => new MetaElement(WrappedDriver.FindElement(metaKeywordsSelector));
        private IWebElement ProductFormElement => WrappedDriver.FindElement(productFormSelector);

        #endregion

        /// <summary>
        /// ViewModel.
        /// </summary>
        public ProductDetailsModel ViewModel
        {
            get
            {
                var model = new ProductDetailsModel
                {
                    Name = ProductNameElement.Text.Trim(),
                    ShortDescription = ShortDescriptionElement.Text.Trim(),
                    FullDescription = FullDescriptionElement.Text.Trim(),
                    MetaDescription = MetaDescriptionElement.Content,
                    MetaKeywords = MetaKeywordsElement.Content,
                    SeName = ProductFormElement.GetAttribute("action")
                };

                return model;
            }
        }

        /// <summary>
        /// Retrieves the bread crumb.
        /// </summary>
        public ProductBreadCrumbComponent BreadCrumb
        {
            get
            {
                return pageObjectFactory.PrepareComponent(
                    new ProductBreadCrumbComponent(PageSettings, WrappedDriver));
            }
        }

        /// <summary>
        /// Checks if the product has a payment plan.
        /// </summary>
        public bool HasPaymentPlan
        {
            get
            {
                return WrappedDriver
                    .FindElements(paymentPlanSelector)
                    .Any();
            }
        }

        /// <summary>
        /// Entering/removing codes on the product. These codes can be coupons,
        /// discounts, access codes, etc...
        /// </summary>
        public ChallengeCodeComponent CodeHandler
        {
            get
            {
                return pageObjectFactory.PrepareComponent(
                    new ChallengeCodeComponent(
                        WrappedDriver,
                        By.CssSelector(".coupon-box")));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves a list of all payment plan options.
        /// </summary>
        /// <returns></returns>
        public IList<IWebElement> GetPaymentPlanOptions()
        {
            return PaymentPlanSelectElement?.Options ?? new List<IWebElement>();
        }

        /// <summary>
        /// Adds the product to the cart.
        /// </summary>
        /// <param name="productAttributesCommand">
        /// An action that will be passed the modal body that appears after
        /// clicking 'Add to cart' that appears asking the user to enter
        /// additional information. If null then the default handler is used.
        /// </param>
        public virtual void AddToCart(Func<IWebElement, IList<string>> productAttributesCommand = null)
        {
            AddToCartButtonElement.Click();

            var openedRemodals = WrappedDriver.FindElements(openRemodalSelector);
        }

        #endregion
    }
}
