using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// Represents the _CreateOrUpdate.ActivityLog.cshtml partial view.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class _CreateOrUpdateActivityLogComponent : PageComponent
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="_CreateOrUpdateActivityLogComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public _CreateOrUpdateActivityLogComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            ActivityLog = new KGridComponent<_CreateOrUpdateActivityLogComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#activitylog-grid"),
                pageObjectFactory,
                driver,
                this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the activity log.
        /// </summary>
        /// <value>
        /// The activity log.
        /// </value>
        public KGridComponent<_CreateOrUpdateActivityLogComponent> ActivityLog { get; }

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
            ActivityLog.Load();

            return this;
        }

        #endregion
    }
}
