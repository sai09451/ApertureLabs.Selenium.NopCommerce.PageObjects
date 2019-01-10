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

        private static IWebDriver Driver;
        private static WebDriverFactory WebDriverFactory;

        private IPageObjectFactory pageObjectFactory;

        #endregion

        #region Setup/Teardown

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            WebDriverFactory = new WebDriverFactory();
            Driver = WebDriverFactory.CreateDriver(
                MajorWebDriver.Chrome,
                WindowSize.DefaultDesktop);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            pageObjectFactory = new PageObjectFactory(Driver);
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
