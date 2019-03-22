using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin;
using PublicPO = ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public;

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Admin.Product
{
    [TestClass]
    public class EditPageTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private AdminPO.Product.IEditPage editPage;
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
                    .First(i => i.GetName() == "Catalog")
                    .Expand()
                        .GetItems()
                        .First(i => i.GetName() == "Products")
                        .Select<AdminPO.Product.IListPage>()
                .GetListedProducts()
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

        [Description("Verifies the ctor throws no exceptions.")]
        [Ignore]
        [TestMethod]
        public void EditPageTest()
        { }

        [TestMethod]
        public void TabsTest()
        {
            var initialTabName = editPage.Tabs.GetActiveTabName();
            editPage.Tabs.SelectTab("Pictures");
            var picturesTabName = editPage.Tabs.GetActiveTabName();
            editPage.Tabs.SelectTab("Product info");
            var finalTabsName = editPage.Tabs.GetActiveTabName();

            Assert.AreEqual(picturesTabName, "Pictures");
            Assert.AreEqual(initialTabName, finalTabsName);
        }

        [TestMethod]
        public void CopyProductTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PreviewTest()
        {
            editPage.Preview();
        }

        [TestMethod]
        public void SaveTest()
        {
            var listPage = editPage.Save();

            Assert.IsNotNull(listPage);
        }

        [TestMethod]
        public void SaveAndContinueEdit()
        {
            var editPageAfter = editPage.SaveAndContinueEdit();

            Assert.IsTrue(editPageAfter.Equals(editPage));
        }

        #endregion
    }
}
