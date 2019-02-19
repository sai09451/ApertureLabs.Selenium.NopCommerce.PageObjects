using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using AdminPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin;
using PublicPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Components.AdminMainHeader
{
    [TestClass]
    public class AdminMainHeaderComponentTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private AdminPO.Home.IHomePage homePage;
        private IAdminMainHeaderComponent mainHeaderComponent;
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
            var homePage = pageObjectFactory.PreparePage<PublicPO.Home.IHomePage>();

            // Login and go to the admin page.
            this.homePage = homePage
                .Login("admin@yourstore.com", "admin")
                .AdminHeaderLinks
                .GoToAdmin();

            mainHeaderComponent = this.homePage.NavigationBar;
        }

        #endregion

        #region Tests

        [Description("Verifies no exceptions are thrown.")]
        [TestMethod]
        public void ClearCacheTest()
        {
            mainHeaderComponent.ClearCache();
        }

        [Description("Verifies no exceptions are thrown.")]
        [TestMethod]
        public void CollapseSidebarTest()
        {
            mainHeaderComponent.CollapseSidebar(true);
            mainHeaderComponent.CollapseSidebar(false);
            mainHeaderComponent.CollapseSidebar(true);
            mainHeaderComponent.CollapseSidebar(true);
        }

        [TestMethod]
        public void GetCurrentUserNameTest()
        {
            var expectedUserName = "John Smith";
            var actualUserName = mainHeaderComponent.GetCurrentUserName();

            Assert.AreEqual(expectedUserName, actualUserName);
        }

        [TestMethod]
        public void GoHomeTest()
        {
            var adminHomePage = mainHeaderComponent.GoHome();

            Assert.IsNotNull(adminHomePage);
        }

        [TestMethod]
        public void PublicStoreTest()
        {
            var publicHomePage = mainHeaderComponent.PublicStore();

            Assert.IsNotNull(publicHomePage);
        }

        [Description("Verifies no exceptions are thrown.")]
        [TestMethod]
        public void RestartApplicationTest()
        {
            mainHeaderComponent.RestartApplication();
        }

        #endregion
    }
}
