using System;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.HeaderLinks;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.HeaderLinks
{
    /// <summary>
    /// HeaderLinksComponent.
    /// </summary>
    public class HeaderLinksComponent : PageComponent, IViewModel<HeaderLinksModel>
    {
        #region Fields

        private readonly IPageObjectFactory pageObjectFactory;

        #region Selectors

        private readonly By CustomerInfoSelector = By.CssSelector(".ico-account");
        private readonly By LogoutSelector = By.CssSelector(".ico-logout");
        private readonly By RegisterSelector = By.CssSelector(".ico-register");
        private readonly By LoginSelector = By.CssSelector(".ico-login");
        private readonly By PrivateMessagesSelector = By.CssSelector(".ico-inbox");
        private readonly By PrivateMessageLabelSelector = By.CssSelector(".ico-inbox .inbox-label");
        private readonly By PrivateMessageQtySelector = By.CssSelector(".ico-inbox .inbox-qty");
        private readonly By WishListSelector = By.CssSelector(".ico-wishlist");
        private readonly By WishListLabelSelector = By.CssSelector(".ico-wisthlist + .wisthlist-label");
        private readonly By WishListQtySelector = By.CssSelector(".ico-wisthlist + .wisthlist-qty");
        private readonly By ShoppingCartSelector = By.CssSelector(".ico-cart");
        private readonly By ShoppingCartLabelSelector = By.CssSelector(".ico-cart + .cart-label");
        private readonly By ShoppingCartQtySelector = By.CssSelector(".ico-cart + .cart-qty");
        private readonly By CurrencySelector = By.CssSelector(".currency-selector > #customerCurrency");
        private readonly By LanguageSelector = By.CssSelector(".language-selector > #customerlanguage");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderLinksComponent"/> class.
        /// </summary>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public HeaderLinksComponent(IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(By.CssSelector(".header-links"), driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement CustomerInfoElement => WrappedDriver.FindElement(CustomerInfoSelector);
        private IWebElement LogoutElement => WrappedDriver.FindElement(LogoutSelector);
        private IWebElement RegisterElement => WrappedDriver.FindElement(RegisterSelector);
        private IWebElement LoginElement => WrappedDriver.FindElement(LoginSelector);
        private IWebElement PrivateMessageElement => WrappedDriver.FindElement(PrivateMessagesSelector);
        private IWebElement PrivateMessageLabelElement => PrivateMessageElement.FindElement(PrivateMessageLabelSelector);
        private IWebElement PrivateMessageQtyElement => PrivateMessageElement.FindElement(PrivateMessageQtySelector);
        private IWebElement WishListElement => WrappedDriver.FindElement(WishListSelector);
        private IWebElement WishListLabelElement => WishListElement.FindElement(WishListLabelSelector);
        private IWebElement WishListQtyElement => WishListElement.FindElement(WishListQtySelector);
        private IWebElement ShoppingCartElement => WrappedDriver.FindElement(ShoppingCartSelector);
        private IWebElement ShoppingCartLabelElement => ShoppingCartElement.FindElement(ShoppingCartLabelSelector);
        private IWebElement ShoppingCartQtyElement => ShoppingCartElement.FindElement(ShoppingCartQtySelector);
        private SelectElement CurrencyElement => new SelectElement(WrappedElement.FindElement(CurrencySelector));
        private SelectElement LanguageElement => new SelectElement(WrappedElement.FindElement(LanguageSelector));

        #endregion

        /// <summary>
        /// Returns true if a user is signed into the site.
        /// </summary>
        public bool IsLoggedIn => WrappedDriver.FindElements(LogoutSelector).Any();

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves the viewmodel.
        /// </summary>
        /// <returns></returns>
        public HeaderLinksModel ViewModel()
        {
            var wait = WrappedDriver.Wait(TimeSpan.FromSeconds(30));

            var isLoggedIn = wait.Exists(LogoutSelector.ToString());
            var hasShoppingCartItems = wait.Exists(ShoppingCartSelector);
            var hasPrivateMessages = wait.Exists(PrivateMessagesSelector);
            var hasWishList = WrappedDriver.FindElements(WishListSelector).Any();

            var model = new HeaderLinksModel
            {
                IsAuthenticated = isLoggedIn,
                CustomerName = isLoggedIn
                    ? CustomerInfoElement.Text
                    : null,
                ShoppingCartEnabled = hasShoppingCartItems,
                ShoppingCartItems = hasShoppingCartItems
                    ? ShoppingCartQtyElement.TextHelper().ExtractInteger()
                    : 0,
                AllowPrivateMessages = hasPrivateMessages,
                UnreadPrivateMessages = hasPrivateMessages
                    ? PrivateMessageLabelElement.Text
                    : null,
                WishlistEnabled = hasWishList,
                WishlistItems = hasWishList
                    ? WishListQtyElement.TextHelper().ExtractInteger()
                    : 0,
                Currency = CurrencyElement.SelectedOption.TextHelper().InnerText,
                Language = LanguageElement.SelectedOption.TextHelper().InnerText
            };

            return model;
        }

        /// <summary>
        /// Sets the currency.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public HeaderLinksComponent SetCurrency(string currency,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var alreadySelected = String.Equals(
                CurrencyElement.SelectedOption.TextHelper().InnerText,
                currency,
                stringComparison);

            if (alreadySelected)
                return this;

            var newEl = CurrencyElement.Options
                .Select((element, index) => new { element, index })
                .FirstOrDefault(opt => String.Equals(
                    opt.element.TextHelper().InnerText,
                    currency,
                    stringComparison));

            if (newEl == null)
                throw new NoSuchElementException();

            CurrencyElement.SelectByIndex(newEl.index);

            return this;
        }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        /// <returns></returns>
        public string GetCurrency()
        {
            return CurrencyElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public HeaderLinksComponent SetLanguage(string language,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var alreadySelected = String.Equals(
                LanguageElement.SelectedOption.TextHelper().InnerText,
                language,
                stringComparison);

            if (alreadySelected)
                return this;

            var newEl = LanguageElement.Options
                .Select((element, index) => new { element, index })
                .FirstOrDefault(opt => String.Equals(
                    opt.element.TextHelper().InnerText,
                    language,
                    stringComparison));

            if (newEl == null)
                throw new NoSuchElementException();

            LanguageElement.SelectByIndex(newEl.index);

            return this;
        }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <returns></returns>
        public string GetLanguage()
        {
            return LanguageElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Logs in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public HomePage Login(string email, string password)
        {
            //var cookie = WrappedDriver.Manage().Cookies
            //    .GetCookieNamed("Nop.Authentication");

            HomePage homePage = null;

            if (IsLoggedIn)
                LogoutElement.Click();

            if (!WrappedDriver.Url.EndsWith("login"))
                LoginElement.Click();

            var loginPage = pageObjectFactory.PreparePage<LoginPage>();

            //loginPage.EnterEmail($"++{email}");
            loginPage.EnterEmail(email);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin(
                _homePage => homePage = _homePage, // On success handler
                _loginPage => throw new Exception( // On error handler
                    String.Join(", ", _loginPage.Errors)));

            return homePage;
        }

        /// <summary>
        /// Goes to the customer info page.
        /// </summary>
        /// <returns></returns>
        public IInfoPage CustomerInfo()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(CustomerInfoElement, e => e.Click());

            return pageObjectFactory.PreparePage<IInfoPage>();
        }

        #endregion
    }
}
