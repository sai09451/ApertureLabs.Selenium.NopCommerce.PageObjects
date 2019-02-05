using System;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Components.OrderSummary
{
    [TestClass]
    public class OrderSummaryComponentTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private ICartPage cartPage;
        private OrderSummaryComponent orderSummary;
        private IPageObjectFactory pageObjectFactory;
        private IWebDriver driver;

        public TestContext TestContext { get; set; }

        #endregion

        #region Setup/Teardown

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            WebDriverFactory = new WebDriverFactory();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            WebDriverFactory.Dispose();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            driver = WebDriverFactory.CreateDriver(
                MajorWebDriver.Chrome,
                WindowSize.DefaultDesktop);

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(driver);
            serviceCollection.AddSingleton(new PageSettings
            {
                BaseUrl = "http://nopcommerce410.local/"
            });

            pageObjectFactory = new PageObjectFactory(serviceCollection);
            var homePage = pageObjectFactory.PreparePage<IHomePage>();

            // This verifies that there are at least two products in the cart.
            cartPage = homePage
                .Login("admin@yourstore.com", "admin")
                .Search("adidas consortium")
                .GetResults()
                .First()
                .GoToProductPage()
                .SetAttribute(
                    term =>
                    {
                        return term.TextHelper().InnerText.StartsWith(
                            "size",
                            StringComparison.OrdinalIgnoreCase);
                    },
                    detail =>
                    {
                        var select = new SelectElement(
                            detail.FindElement(
                                By.CssSelector("select")));

                        select.SelectByIndex(2);
                    })
                .AddToCart()
                .SetAttribute(
                    term =>
                    {
                        return term.TextHelper().InnerText.StartsWith(
                            "color",
                            StringComparison.OrdinalIgnoreCase);
                    },
                    detail =>
                    {
                        var blueColor = detail.FindElement(
                            By.CssSelector("li:nth-child(2) label"));

                        blueColor.Click();
                    })
                .SetQuantity(4)
                .AddToCart()
                .GoToShoppingCart();

            orderSummary = cartPage.OrderSummary;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (cartPage != null)
            {
                driver
                .Navigate()
                .GoToUrl(cartPage.Uri);

                cartPage.Load();
                var cartItems = cartPage.OrderSummary.GetCartItems();

                foreach (var cartItem in cartItems)
                    cartItem.MarkForRemoval(true);

                cartPage.OrderSummary.UpdateShoppingCart();
            }
        }

        #endregion

        #region Methods

        [Description("Verifies no errors are thrown when accessing the" +
            "component.")]
        [TestMethod]
        public void OrderSummaryComponentTest()
        { }

        [TestMethod]
        public void GetCartItemTest()
        {
            var item = orderSummary.GetCartItem(0);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void GetCartItemsTest()
        {
            var items = orderSummary.GetCartItems().ToArray();

            Assert.IsTrue(items.Any());
            CollectionAssert.AllItemsAreNotNull(items);
        }

        [TestMethod]
        public void RemoveProductTest()
        {
            var itemCountBefore = orderSummary.GetCartItems().Count;
            orderSummary.RemoveProduct(0);
            var itemCountAfter = orderSummary.GetCartItems().Count;

            Assert.IsTrue(itemCountAfter < itemCountBefore);
        }

        [TestMethod]
        public void ContinueShoppingTest()
        {
            // This will set the last search page to IProductsByCategoryPage.
            var shoesPage = cartPage
                .Search("test")
                .SelectParentCategory("Apparel")
                .SelectCategory("Shoes");
                
            shoesPage.GoToShoppingCart();

            var page = orderSummary.ContinueShopping<IProductsByCategoryPage>();

            Assert.IsNotNull(page);
            Assert.AreEqual(shoesPage, page);
        }

        [Ignore("Same as the RemoveProductTest.")]
        [TestMethod]
        public void UpdateShoppingCartTest()
        {
            RemoveProductTest();
        }

        [TestMethod]
        public void AcceptTermsAndConditionsTest()
        {
            var el = new CheckboxElement(
                driver.FindElement(
                    By.CssSelector("#termsofservice")));

            var isCheckedBefore = el.IsChecked;
            orderSummary.AcceptTermsAndConditions(true);
            var isCheckedAfter = el.IsChecked;

            Assert.IsFalse(isCheckedBefore);
            Assert.IsTrue(isCheckedAfter);
        }

        #endregion
    }
}
