using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// Represents the _CreateOrUpdate.Orders.cshtml page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class _CreateOrUpdateOrdersComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By viewButtonSelector = By.CssSelector("td:last-child > *");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="_CreateOrUpdateOrdersComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public _CreateOrUpdateOrdersComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            OrdersGrid = new KGridComponent<_CreateOrUpdateOrdersComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#order-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the orders grid.
        /// </summary>
        /// <value>
        /// The orders grid.
        /// </value>
        public KGridComponent<_CreateOrUpdateOrdersComponent> OrdersGrid { get; }

        #endregion

        #region Methods

        /// <summary>
        /// If overloaded don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            OrdersGrid.Load();

            return this;
        }

        /// <summary>
        /// Views the order. Need to pass in a row from the OrdersGrids.
        /// </summary>
        /// <param name="gridRow">The grid row.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Order.IEditPage ViewOrder(IWebElement gridRow)
        {
            if (gridRow == null)
                throw new ArgumentException(nameof(gridRow));

            var hasCorrectTagName = String.Equals(
                gridRow.TagName,
                "tr",
                StringComparison.OrdinalIgnoreCase);

            if (!hasCorrectTagName)
            {
                throw new UnexpectedTagNameException("Expected 'tr' but " +
                    $"element had '{gridRow.TagName}' instead.");
            }

            var bttn = gridRow.FindElement(viewButtonSelector);

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(bttn, e => e.Click());

            return pageObjectFactory.PreparePage<Order.IEditPage>();
        }

        #endregion
    }
}
