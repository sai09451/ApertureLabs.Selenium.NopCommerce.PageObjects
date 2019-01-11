using System;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks.Tests
{
    [TestClass]
    public class AdminHeaderLinksComponentTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private IPageObjectFactory pageObjectFactory;
        private IWebDriver driver;

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
                BaseUrl = "http://local.aperturelabs.nop-demo-store-a.biz"
            });

            pageObjectFactory = new PageObjectFactory(serviceCollection);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Dispose();
        }

        #endregion

        #region Tests

        [TestMethod]
        public void AdminHeaderLinksComponentTest()
        {
            var homePage = pageObjectFactory.PreparePage<HomePage>();
            homePage.Login("alex.hayes@aperturelabs.biz", "password");
            var headerLinks = homePage.AdminHeaderLinks;

            Assert.IsNotNull(headerLinks);
        }

        [TestMethod]
        public void GoToAdminTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CanManagePageTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ManagePageTest()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
