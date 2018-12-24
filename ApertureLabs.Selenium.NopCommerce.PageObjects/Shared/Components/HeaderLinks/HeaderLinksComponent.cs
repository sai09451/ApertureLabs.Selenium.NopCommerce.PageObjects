using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.HeaderLinks;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.HeaderLinks
{
    /// <summary>
    /// HeaderLinksComponent.
    /// </summary>
    public class HeaderLinksComponent : PageComponent, IViewModel<HeaderLinksModel>
    {
        #region Fields

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

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        public HeaderLinksComponent(IWebDriver driver)
            : base(driver, By.CssSelector(".header-links"))
        { }

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

        #endregion

        /// <inheritdoc/>
        public HeaderLinksModel ViewModel
        {
            get
            {
                var wait = WrappedDriver.Wait(TimeSpan.FromSeconds(30));

                var isLoggedIn = wait.Exists(LogoutSelector.ToString());
                var hasShoppingCartItems = wait.Exists(ShoppingCartSelector);
                var hasPrivateMessages = wait.Exists(PrivateMessagesSelector);

                var model = new HeaderLinksModel
                {
                    IsAuthenticated = isLoggedIn,
                    CustomerName = isLoggedIn
                        ? CustomerInfoElement.Text
                        : null,
                    ShoppingCartEnabled = hasShoppingCartItems,
                    ShoppingCartItems = hasShoppingCartItems
                        ? ShoppingCartQtyElement.GetTextHelper().ExtractInteger()
                        : 0,
                    AllowPrivateMessages = hasPrivateMessages,
                    UnreadPrivateMessages = hasPrivateMessages
                        ? PrivateMessageLabelElement.Text
                        : null
                };

                return model;
            }
        }

        /// <summary>
        /// Returns true if a user is signed into the site.
        /// </summary>
        public bool IsLoggedIn => WrappedDriver.FindElements(LogoutSelector).Any();

        #endregion

        #region Methods

        /// <summary>
        /// Logs in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public HomePage Login(string email, string password)
        {
            var cookie = WrappedDriver.Manage().Cookies
                .GetCookieNamed("Nop.Customer");

            HomePage homePage = null;

            var loginPage = new LoginPage(WrappedDriver, null);
            loginPage.Load(true);

            //loginPage.EnterEmail($"++{email}");
            loginPage.EnterEmail(email);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin(
                _homePage => homePage = _homePage, // On success handler
                _loginPage => throw new Exception( // On error handler
                    String.Join(", ",
                    _loginPage.Errors)));

            return homePage;
        }

        /// <inheritdoc/>
        public override bool IsStale()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
