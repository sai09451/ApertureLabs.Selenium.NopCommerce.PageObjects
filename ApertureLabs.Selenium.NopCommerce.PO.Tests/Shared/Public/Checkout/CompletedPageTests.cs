using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Public.Checkout
{
    [TestClass]
    public class CompletedPageTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private ICompletedPage completedPage;
        private IPageObjectFactory pageObjectFactory;
        private IWebDriver driver;

        public TestContext TestContext { get; set; }

        #endregion

        #region Setup/Cleanup

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
                BaseUrl = new Uri("http://nopcommerce410.local/")
            });

            pageObjectFactory = new PageObjectFactory(serviceCollection);
            var homePage = pageObjectFactory.PreparePage<IHomePage>();

            // This verifies that there are at least two products in the cart.
            completedPage = homePage
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
                .GoToShoppingCart()
                .OrderSummary
                .AcceptTermsAndConditions(true)
                .FullCheckout();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (completedPage != null)
            {
                var cartPage = completedPage.GoToShoppingCart();
                var items = cartPage.OrderSummary.GetCartItems();

                if (items.Any())
                {
                    foreach (var item in items)
                        item.MarkForRemoval(true);

                    cartPage.OrderSummary.UpdateShoppingCart();
                }
            }
        }

        #endregion

        #region Tests

        [Description("Verifies no exeptions are thrown.")]
        [TestMethod]
        public void CompletedPageTest()
        { }

        [TestMethod]
        public void ContinueTest()
        {
            var homePage = completedPage.Continue();

            Assert.IsNotNull(homePage);
            Assert.IsFalse(homePage.IsStale());
        }

        [TestMethod]
        public void GetOrderNumberTest()
        {
            var orderNumber = completedPage.GetOrderNumber();

            Assert.IsTrue(orderNumber > 0);
        }

        [TestMethod]
        public void ViewOrderDetailsTest()
        {
            var orderDetailsPage = completedPage.ViewOrderDetails();

            Assert.IsNotNull(orderDetailsPage);
            Assert.IsFalse(orderDetailsPage.IsStale());
        }

        #endregion
    }
}
