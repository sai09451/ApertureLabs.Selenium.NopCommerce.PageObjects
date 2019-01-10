using System;
using ApertureLabs.Selenium.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks.Tests
{
    [TestClass]
    public class AdminHeaderLinksComponentTests
    {
        #region Fields

        private static IPageObjectFactory PageObjectFactory;
        private static IWebDriver Driver;
        private static WebDriverFactory WebDriverFactory;

        #endregion

        #region Setup/Teardown

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            PageObjectFactory = new PageObjectFactory();
            WebDriverFactory = new WebDriverFactory();
            Driver = WebDriverFactory.CreateDriver(
                MajorWebDriver.Chrome,
                WindowSize.DefaultDesktop);
        }

        #endregion

        #region Tests

        [TestMethod]
        public void AdminHeaderLinksComponentTest()
        {
            throw new NotImplementedException();
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
