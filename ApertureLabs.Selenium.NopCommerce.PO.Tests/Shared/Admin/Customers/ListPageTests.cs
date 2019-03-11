using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Customers;
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

namespace ApertureLabs.Selenium.NopCommerce.PO.Tests.Shared.Admin.Customers
{
    [TestClass]
    public class ListPageTests
    {
        #region Fields

        private static WebDriverFactory WebDriverFactory;

        private AdminPO.Customers.IListPage listPage;
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

            // Login and go to the admin page. Then go to the admin customers
            // list page.
            listPage = homePage
                .Login("admin@yourstore.com", "password")
                .AdminHeaderLinks
                .GoToAdmin()
                .MainSideBar
                    .GetItems()
                    .First(i => i.GetName() == "Customers")
                    .Expand()
                        .GetItems()
                        .First(i => i.GetName() == "Customers")
                        .Select<AdminPO.Customers.IListPage>();
        }

        #endregion

        #region Tests

        [Description("Verifies the customers grid isn't null.")]
        [TestMethod]
        public void CustomersGridTest()
        {
            Assert.IsNotNull(listPage.CustomersGrid);
        }

        [TestMethod]
        public void SearchTest()
        {
            var listedCustomers = listPage
                .Search(new CustomerSearchModel { Email = "steve_gates" })
                .GetListedCustomers()
                .ToList();

            var steveCustomer = listedCustomers.First();

            Assert.AreEqual(steveCustomer.GetName(), "Steve Gates");
        }

        [Description("Verifies no exceptions are thrown.")]
        [TestMethod]
        public void ExportToTest()
        {
            listPage.ExportTo(
                type: "Export to XML (all found)",
                downloadsPath: "~/Downloads",
                expectedFileName: "customers.xml");
        }

        #endregion
    }
}
