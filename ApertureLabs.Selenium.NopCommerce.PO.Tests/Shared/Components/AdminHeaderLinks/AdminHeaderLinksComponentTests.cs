using System;
using System.Linq;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks
{
    [TestClass]
    public class AdminHeaderLinksComponentTests
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
        public void AdminHeaderLinksComponentTest()
        {
            var homePage = pageObjectFactory.PreparePage<IHomePage>();
            homePage.Login("admin@yourstore.com", "admin");
            var headerLinks = homePage.AdminHeaderLinks;

            Assert.IsNotNull(headerLinks);
        }

        [TestMethod]
        public void GoToAdminTest()
        {
            var homePage = pageObjectFactory.PreparePage<IHomePage>();
            homePage.Login("admin@yourstore.com", "admin");
            var adminHomePage = homePage.AdminHeaderLinks.GoToAdmin();

            Assert.IsNotNull(adminHomePage);
        }

        [TestMethod]
        public void CanManagePageTest()
        {
            var homePage = pageObjectFactory.PreparePage<IHomePage>();
            homePage.Login("admin@yourstore.com", "admin");
            var canManageHomePage = homePage.AdminHeaderLinks.CanManagePage();

            Assert.IsFalse(canManageHomePage);
        }

        [TestMethod]
        public void ManagePageTest()
        {
            var importedModules = pageObjectFactory.GetImportedModules();
            var homePage = pageObjectFactory.PreparePage<IHomePage>();
            var adminProductPage = homePage
                .Login("admin@yourstore.com", "admin")
                .Search("leica")
                .GetResults()
                .First()
                .GoToProductPage()
                .AdminHeaderLinks
                .ManagePage();

            Assert.IsNotNull(adminProductPage);
        }

        #endregion
    }
}
