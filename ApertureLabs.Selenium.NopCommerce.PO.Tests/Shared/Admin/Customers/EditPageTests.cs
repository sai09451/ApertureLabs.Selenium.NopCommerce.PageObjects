using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using AdminPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin;
using PublicPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Admin.Customers
{
    [TestClass]
    public class EditPageTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private AdminPO.Customers.IEditPage editPage;
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
            var loginPage = pageObjectFactory.PreparePage<PublicPO.Customer.ILoginPage>();

            // Login and go to the admin page. Then go to the admin customers
            // list page.
            editPage = loginPage
                .Login("admin@yourstore.com", "password")
                .AdminHeaderLinks
                .GoToAdmin()
                .MainSideBar
                    .GetItems()
                    .First(i => i.GetName() == "Customers")
                    .Expand()
                        .GetItems()
                        .First(i => i.GetName() == "Customers")
                        .Select<AdminPO.Customers.IListPage>()
                .GetListedCustomers()
                .First()
                .Edit();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            try
            {
                driver?.Dispose();
            }
            catch (ObjectDisposedException)
            { }
        }

        #endregion

        #region Tests

        [Description("Verifies the ctor doesn't throw any exceptions.")]
        [Ignore]
        [TestMethod]
        public void EditPageTest()
        { }

        [TestMethod]
        public void BackToCustomerListTest()
        {
            var listPage = editPage.BackToCustomerList();

            Assert.IsNotNull(listPage);
        }

        [TestMethod]
        public void SaveTest()
        {
            var listPage = editPage.Save();

            Assert.IsTrue(listPage.HasNotifications());
            Assert.IsNotNull(listPage);
        }

        [TestMethod]
        public void SaveAndContinueTest()
        {
            var editPageAfter = editPage.SaveAndContinueEdit();

            Assert.IsTrue(editPageAfter.HasNotifications());
            Assert.AreSame(editPageAfter, editPage);
        }

        [TestMethod]
        public void TabsTest()
        {
            var infoTab = editPage.Tabs.SelectTab<AdminPO.Customers._CreateOrUpdateInfoComponent>(
                "Customer info",
                StringComparison.Ordinal);

            var email = infoTab.GetEmail();

            var activiyLogTab = editPage.Tabs.SelectTab<AdminPO.Customers._CreateOrUpdateActivityLogComponent>(
                "Activity log",
                StringComparison.Ordinal);

            Assert.IsFalse(String.IsNullOrEmpty(email));
            Assert.IsNotNull(activiyLogTab);
        }

        #endregion
    }
}
