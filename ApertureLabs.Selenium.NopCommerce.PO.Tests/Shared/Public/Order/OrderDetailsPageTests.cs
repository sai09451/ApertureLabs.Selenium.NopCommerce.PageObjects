using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Public.Order
{
    [TestClass]
    public class OrderDetailsPageTests
    {
        #region Fields

        private static IOrderDetailsPage orderDetailsPage;
        private static IPageObjectFactory pageObjectFactory;
        private static IWebDriver driver;
        private static WebDriverFactory WebDriverFactory;

        public TestContext TestContext { get; set; }

        #endregion

        #region Setup/Cleanup

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            WebDriverFactory = new WebDriverFactory();

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
            orderDetailsPage = homePage
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
                .FullCheckout()
                .ViewOrderDetails();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            if (orderDetailsPage != null)
            {
                var cartPage = orderDetailsPage.GoToShoppingCart();
                var items = cartPage.OrderSummary.GetCartItems();

                if (items.Any())
                {
                    foreach (var item in items)
                        item.MarkForRemoval(true);

                    cartPage.OrderSummary.UpdateShoppingCart();
                }
            }

            WebDriverFactory.Dispose();
        }

        #endregion

        #region Tests

        [Description("Verifies that no exceptions are thrown.")]
        [TestMethod]
        public void OrderDetailsPageTest()
        { }

        [TestMethod]
        public void GetBillingAddressTest()
        {
            var address = orderDetailsPage.GetBillingAddress();

            Assert.IsNotNull(address);
            Assert.IsFalse(String.IsNullOrEmpty(address.Address1));
        }

        [TestMethod]
        public void GetOrderDateTest()
        {
            var date = orderDetailsPage.GetOrderDate();

            Assert.AreNotEqual(date, default(DateTime));
        }

        [TestMethod]
        public void GetOrderNumberTest()
        {
            var orderNumber = orderDetailsPage.GetOrderNumber();

            Assert.IsTrue(orderNumber > 0);
        }

        [TestMethod]
        public void GetOrderStatusTest()
        {
            var orderStatus = orderDetailsPage.GetOrderStatus();

            Assert.IsFalse(String.IsNullOrEmpty(orderStatus));
        }

        [TestMethod]
        public void GetOrderTotalTest()
        {
            var orderTotal = orderDetailsPage.GetOrderTotal();

            Assert.IsTrue(orderTotal > 0);
        }

        [TestMethod]
        public void GetProductsTest()
        {
            var products = orderDetailsPage.GetProducts();

            Assert.IsTrue(products.Count() > 0);
        }

        [TestMethod]
        public void GetShippingCostTest()
        {
            var shipping = orderDetailsPage.GetShippingCost();

            Assert.AreEqual(0, shipping);
        }

        [TestMethod]
        public void GetShippingAddressTest()
        {
            var address = orderDetailsPage.GetShippingAddress();

            Assert.IsNotNull(address);
            Assert.IsFalse(String.IsNullOrEmpty(address.Address1));
        }

        [TestMethod]
        public void GetShippingMethodTest()
        {
            var shippingMethod = orderDetailsPage.GetShippingMethod();

            Assert.IsFalse(String.IsNullOrEmpty(shippingMethod));
        }

        public void GetShippingStatusTest()
        {
            var shippingStatus = orderDetailsPage.GetShippingStatus();

            Assert.IsFalse(String.IsNullOrEmpty(shippingStatus));
        }

        #endregion
    }
}
