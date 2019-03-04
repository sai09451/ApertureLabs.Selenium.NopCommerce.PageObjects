using System;
using System.Collections.Generic;
using System.Text;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Public.Home
{
    [TestClass]
    public class HomePageTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

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
                BaseUrl = new Uri("http://nopcommerce410.local/")
            });

            pageObjectFactory = new PageObjectFactory(serviceCollection);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver?.Dispose();
        }

        #endregion

        #region Tests

        [TestMethod]
        public void GetFeaturedProductsTests()
        {
            var homePage = pageObjectFactory.PreparePage<IHomePage>();
            var featuredProducts = homePage.GetFeaturedProducts();
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetNewsTest()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
