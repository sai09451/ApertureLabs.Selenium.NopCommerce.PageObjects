using System;
using System.Linq;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using AdminPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin;
using PublicPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Components.AdminMainSideBar
{
    [TestClass]
    public class AdminMainSideBarComponentTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private AdminPO.Home.IHomePage homePage;
        private IAdminMainSideBarComponent adminMainSideBarComponent;
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
            var homePage = pageObjectFactory.PreparePage<PublicPO.Home.IHomePage>();

            // Login and go to the admin page.
            this.homePage = homePage
                .Login("admin@yourstore.com", "admin")
                .AdminHeaderLinks
                .GoToAdmin();

            adminMainSideBarComponent = this.homePage.MainSideBar;
        }

        #endregion

        #region Tests

        [TestMethod]
        public void GetItemsTest()
        {
            var productListPage = adminMainSideBarComponent.GetItems()
                .FirstOrDefault(item => item.GetName() == "Catalog")
                .GetItems()
                .FirstOrDefault(item => item.GetName() == "Products")
                .Select<AdminPO.Product.IListPage>();

            Assert.IsNotNull(productListPage);
        }

        [TestMethod]
        public void SearchTest()
        {
            var productListPage = adminMainSideBarComponent
                .Search<AdminPO.Product.IListPage>("Products");

            Assert.IsNotNull(productListPage);
        }

        #endregion
    }
}
