using System;
using System.Linq;
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
    public class OnePageCheckoutPageTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private ICheckoutStepPage checkoutPage;
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
                BaseUrl = "http://nopcommerce410.local/"
            });

            pageObjectFactory = new PageObjectFactory(serviceCollection);
            var homePage = pageObjectFactory.PreparePage<IHomePage>();

            // This verifies that there are at least two products in the cart.
            checkoutPage = homePage
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
                .ProceedToCheckout<ICheckoutStepPage>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (checkoutPage != null)
            {
                var cartPage = checkoutPage.GoToShoppingCart();
                var items = cartPage.OrderSummary.GetCartItems();

                foreach (var item in items)
                    item.MarkForRemoval(true);

                cartPage.OrderSummary.UpdateShoppingCart();
            }
        }

        #endregion

        #region Tests

        [Description("Verifies no errors are thrown.")]
        [TestMethod]
        public void OnePageCheckoutPageTest()
        { }

        [TestMethod]
        public void GetCurrentStepTest()
        {
            var initialStep = checkoutPage.GetCurrentStep();

            // The step should be zero.
            Assert.AreEqual(initialStep, 0);
        }

        [TestMethod]
        public void GetTotalStepsSteps()
        {
            var totalSteps = checkoutPage.GetTotalSteps();

            Assert.AreEqual(totalSteps, 6);
        }

        [TestMethod]
        public void GetCurrentStepNameTest()
        {
            var name = checkoutPage.GetCurrentStepName();

            Assert.AreEqual(name, "Billing address");
        }

        [TestMethod]
        public void GetAllStepNamesTest()
        {
            var names = checkoutPage.GetAllStepNames().ToArray();

            CollectionAssert.AreEqual(
                names,
                new[] {
                    "Billing address",
                    "Shipping address",
                    "Shipping method",
                    "Payment method",
                    "Payment information",
                    "Confirm order"
                });
        }

        [Description("Verifies no exceptions are thrown.")]
        [TestMethod]
        public void EnterBillingAddressTest()
        {
            var address = new AddressModel
            {
                Address1 = "W223N608 Make Believe Ln",
                City = "New Berlin",
                Company = "Aperture Labs",
                Country = "United States",
                Email = "vader@gmail.com",
                FirstName = "Darth",
                LastName = "Vader",
                PhoneNumber = "1112223333",
                StateProvince = "Wisconsin",
                ZipPostalCode = "53186"
            };

            checkoutPage.EnterBillingAddress(address, false);
        }

        [Description("Verifies no exceptions are thrown.")]
        [TestMethod]
        public void UseExistingBillingAddressTest()
        {
            checkoutPage.UseExistingBillingAddress(false);
            var addressModel = checkoutPage.GetBillingAddress();

            Assert.IsFalse(String.IsNullOrEmpty(addressModel.Address1));
        }

        [TestMethod]
        public void TryGoToStepTest()
        {
            // Enter a billing address.
            EnterBillingAddressTest();
            var changedSteps = checkoutPage.TryGoToStep("Shipping address");

            Assert.IsTrue(changedSteps);
        }

        [Description("Verifies no exceptions are thrown.")]
        [TestMethod]
        public void EnterShippingAddressTest()
        {
            EnterBillingAddressTest();
            checkoutPage.TryGoToStep(
                stepName: "Shipping address",
                reject: page => throw new Exception());

            var address = new AddressModel
            {
                Address1 = "W223N608 Make Believe Ln",
                City = "Waukesha",
                Company = "",
                Country = "United States",
                Email = "palpatine@gmail.com",
                FirstName = "Darth",
                LastName = "Sidious",
                PhoneNumber = "1112223333",
                StateProvince = "Wisconsin",
                ZipPostalCode = "53186"
            };

            checkoutPage.EnterShippingAddress(address);
            checkoutPage.TryGoToStep(
                stepName: "Shipping method",
                reject: page => throw new Exception());
        }

        [TestMethod]
        public void UseExistingShippingAddressTest()
        {
            EnterBillingAddressTest();
            checkoutPage.TryGoToStep("Shipping address");
            checkoutPage.UseExistingShippingAddress();
            var address = checkoutPage.GetShippingAddress();
            checkoutPage.TryGoToStep(
                stepName: "Shipping method",
                reject: page => throw new Exception());

            Assert.IsFalse(String.IsNullOrEmpty(address.Address1));
        }

        [Description("Verifies no exceptions are thrown.")]
        [TestMethod]
        public void SelectShippingMethodTest()
        {
            checkoutPage.UseExistingBillingAddress(true);
            checkoutPage.TryGoToStep(
                stepName: "Shipping method",
                reject: page => throw new Exception());
            checkoutPage.SelectShippingMethod("Ground");
            checkoutPage.TryGoToStep("Payment method");
        }

        [TestMethod]
        public void SelectPaymentMethodTest()
        {
            SelectShippingMethodTest();
            checkoutPage.SelectPaymentMethod("Check / Money Order");
            checkoutPage.TryGoToStep(
                stepName: "Payment information",
                reject: page => throw new Exception());
        }

        [TestMethod]
        public void EnterPaymentInformationTest()
        {
            SelectPaymentMethodTest();
            checkoutPage.EnterPaymentInformation();
            checkoutPage.TryGoToStep(
                stepName: "Confirm order",
                reject: page => throw new Exception());
        }

        [TestMethod]
        public void ConfirmTest()
        {
            EnterPaymentInformationTest();
            var orderDetailsPage = checkoutPage.Confirm();

            Assert.IsNotNull(orderDetailsPage);
        }

        #endregion
    }
}
