using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using AdminPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin;
using PublicPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Admin.Home
{
    [TestClass]
    public class HomePageTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private AdminPO.Home.IHomePage homePage;
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
        }

        #endregion

        #region Tests

        [Description("Verifies the MainSideBarComponent isn't null.")]
        [TestMethod]
        public void MainSideBarTest()
        {
            Assert.IsNotNull(homePage.MainSideBar);
        }

        [Description("Verifies the NavigationBarComponent isn't null.")]
        [TestMethod]
        public void NavigationBarTest()
        {
            Assert.IsNotNull(homePage.NavigationBar);
        }

        [TestMethod]
        public void FooterTest()
        {
            Assert.IsNotNull(homePage.Footer);
        }

        [TestMethod]
        public void BackToTopTest()
        {
            homePage.Footer.WrappedElement.TryScrollToCenter();

            homePage.BackToTop();
        }

        #endregion
    }
}
