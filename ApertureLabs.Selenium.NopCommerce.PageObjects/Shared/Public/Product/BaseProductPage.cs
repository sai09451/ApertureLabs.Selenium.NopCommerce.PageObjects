using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.ProductBreadCrumb;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.DescriptionList;
using ApertureLabs.Selenium.WebElements.Inputs;
using ApertureLabs.Selenium.WebElements.Meta;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product
{
    /// <summary>
    /// Base class for the product pages.
    /// </summary>
    public class BaseProductPage : ParameterPageObject, IBaseProductPage
    {
        #region Fields

        #region Selectors

        private readonly By addToCartSelector = By.CssSelector(".add-to-cart-button");
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
        private readonly By attributesSelector = By.CssSelector(".attributes dl");
        private readonly By tagsSelector = By.CssSelector(".product-tags-list .tag");
        private readonly By quantitySelector = By.CssSelector(".qty-input");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseProductPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public BaseProductPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  pageSettings.BaseUrl,
                  new UriTemplate("{productname}"))
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;
            this.pageSettings = pageSettings;
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AddToCartButtonElement => WrappedDriver
            .FindElement(addToCartSelector);

        private SelectElement PaymentPlanSelectElement => new SelectElement(
            WrappedDriver.FindElement(
                paymentPlanSelector));

        private IReadOnlyCollection<IWebElement> InstallmentPlanElements => WrappedDriver
            .FindElements(installmentPlansSelector);

        private SelectElement SubscriptionPlanElement => new SelectElement(
            WrappedDriver.FindElement(
                subscriptionPlanSelector));

        private IReadOnlyCollection<IWebElement> SubscriptionDetailElements => WrappedDriver
            .FindElements(subscriptionDetailsSelector);

        private IWebElement OpenedRemodalElement => WrappedDriver
            .FindElement(openRemodalSelector);

        private IWebElement ProductNameElement => WrappedDriver
            .FindElement(productNameSelector);

        private IWebElement FullDescriptionElement => WrappedDriver
            .FindElement(fullDescriptionSelector);

        private IWebElement ShortDescriptionElement => WrappedDriver
            .FindElement(shortDescriptionSelector);

        private MetaElement MetaDescriptionElement => new MetaElement(
            WrappedDriver.FindElement(
                metaDescriptionSelector));

        private MetaElement MetaKeywordsElement => new MetaElement(
            WrappedDriver.FindElement(
                metaKeywordsSelector));

        private IWebElement ProductFormElement => WrappedDriver
            .FindElement(productFormSelector);

        private DescriptionListElement Attributes => new DescriptionListElement(
            WrappedDriver.FindElement(
                attributesSelector));

        private IReadOnlyCollection<IWebElement> TagElements => WrappedDriver
            .FindElements(tagsSelector);

        private InputElement QuantityElement => new InputElement(
            WrappedDriver.FindElement(
                quantitySelector));

        #endregion

        /// <summary>
        /// Retrieves the bread crumb.
        /// </summary>
        public virtual ProductBreadCrumbComponent BreadCrumb
        {
            get
            {
                return pageObjectFactory.PrepareComponent(
                    new ProductBreadCrumbComponent(pageObjectFactory,
                        WrappedDriver,
                        pageSettings));
            }
        }

        /// <summary>
        /// Checks if the product has a payment plan.
        /// </summary>
        public virtual bool HasPaymentPlan
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
        public virtual ChallengeCodeComponent CodeHandler
        {
            get
            {
                return pageObjectFactory.PrepareComponent(
                    new ChallengeCodeComponent(
                        WrappedDriver,
                        By.CssSelector(".coupon-box")));
            }
        }

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public virtual IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

        #endregion

        #region Methods

        /// <summary>
        /// If overridding this don't forget to call base.Load().
        /// NOTE: Will navigate to the pages url if the current drivers url
        /// is empty.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// If the driver is an EventFiringWebDriver an event listener will
        /// be added to the 'Navigated' event and uses the url to determine
        /// if the page is 'stale'.
        /// </remarks>
        public override ILoadableComponent Load()
        {
            base.Load();
            basePage.Load();

            return this;
        }

        /// <summary>
        /// ViewModel.
        /// </summary>
        public virtual ProductDetailsModel ViewModel()
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

        /// <summary>
        /// Retrieves a list of all payment plan options.
        /// </summary>
        /// <returns></returns>
        public virtual IList<IWebElement> GetPaymentPlanOptions()
        {
            return PaymentPlanSelectElement?.Options ?? new List<IWebElement>();
        }

        /// <summary>
        /// Adds the product to the cart.
        /// </summary>
        public virtual IBaseProductPage AddToCart()
        {
            AddToCart(
                resolve: page => { },
                reject: page =>
                {
                    throw new Exception("Failed to add the product to the cart");
                });

            return this;
        }

        /// <summary>
        /// Adds to cart and calls resolve/reject if the product was or wasn't
        /// added to the cart.
        /// </summary>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        /// <returns></returns>
        public virtual IBaseProductPage AddToCart(
            Action<IBaseProductPage> resolve,
            Action<IBaseProductPage> reject)
        {
            AddToCartButtonElement.Click();

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(30))
                .Until(d => HasNotifications());

            HandleNotification(el =>
            {
                var hasError = el.Classes().Contains("error");

                DismissNotifications();

                if (hasError)
                    reject(this);
                else
                    resolve(this);
            });

            return this;
        }

        /// <summary>
        /// Goes to the shopping cart page.
        /// </summary>
        /// <returns></returns>
        public virtual ICartPage GoToShoppingCart()
        {
            return basePage.GoToShoppingCart();
        }

        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLoggedIn()
        {
            return basePage.IsLoggedIn();
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual IHomePage Login(string email, string password)
        {
            return basePage.Login(email, password);
        }

        /// <summary>
        /// Logs a user out if logged in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T Logout<T>() where T : IPageObject
        {
            return basePage.Logout<T>();
        }

        /// <summary>
        /// Used to search for a product.
        /// </summary>
        /// <param name="searchFor">Partial or full name of product.</param>
        /// <returns></returns>
        public virtual ISearchPage Search(string searchFor)
        {
            return basePage.Search(searchFor);
        }

        /// <summary>
        /// Similar to <c>Search</c> but waits for the ajax results to resolve
        /// and returns those items.
        /// </summary>
        /// <param name="searchFor">The search for.</param>
        /// <returns></returns>
        public virtual IReadOnlyCollection<IWebElement> SearchAjax(string searchFor)
        {
            return basePage.SearchAjax(searchFor);
        }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="attributeElementHandler">The attribute element handler.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// attributeName
        /// or
        /// attributeElementHandler
        /// </exception>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual IBaseProductPage SetAttribute(string attributeName,
            Action<IWebElement> attributeElementHandler,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (String.IsNullOrEmpty(attributeName))
                throw new ArgumentNullException(nameof(attributeName));
            else if (attributeElementHandler == null)
                throw new ArgumentNullException(nameof(attributeElementHandler));

            var (term, detail) = Attributes
                .GetDescriptions()
                .FirstOrDefault(desc => String.Equals(
                    desc.term.TextHelper().InnerText,
                    attributeName,
                    stringComparison));

            if (detail == null)
                throw new NoSuchElementException();

            attributeElementHandler(detail);

            return this;
        }

        /// <summary>
        /// Sets the attribute. The termPredicate will be passed in the term
        /// element (dt) of each term to determine which attribute to set.
        /// </summary>
        /// <param name="termPredicate">The term predicate.</param>
        /// <param name="attributeElementHandler">The attribute element handler.</param>
        /// <returns></returns>
        public virtual IBaseProductPage SetAttribute(
            Predicate<IWebElement> termPredicate,
            Action<IWebElement> attributeElementHandler)
        {
            if (termPredicate == null)
                throw new ArgumentNullException(nameof(termPredicate));
            else if (attributeElementHandler == null)
                throw new ArgumentNullException(nameof(attributeElementHandler));

            var (term, detail) = Attributes
                .GetDescriptions()
                .FirstOrDefault(desc => termPredicate(desc.term));

            if (detail == null)
                throw new NoSuchElementException();

            attributeElementHandler(detail);

            return this;
        }

        /// <summary>
        /// Gets the full description.
        /// </summary>
        /// <returns></returns>
        public virtual string GetFullDescription()
        {
            return FullDescriptionElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the short description.
        /// </summary>
        /// <returns></returns>
        public virtual string GetShortDescription()
        {
            return ShortDescriptionElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the product tags.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetProductTags()
        {
            return TagElements
                .Select(e => e.TextHelper().InnerText);
        }

        /// <summary>
        /// Sets the quantity.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns></returns>
        public virtual IBaseProductPage SetQuantity(int quantity)
        {
            QuantityElement.SetValue(quantity);

            return this;
        }

        /// <summary>
        /// Determines whether this instance has notifications.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </returns>
        public bool HasNotifications()
        {
            return basePage.HasNotifications();
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        public void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        #endregion
    }
}
