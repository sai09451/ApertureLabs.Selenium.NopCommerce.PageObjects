using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Factories
{
    /// <summary>
    /// CustomPageObjectFactory.
    /// </summary>
    public class CustomPageObjectFactory : PageObjectFactory
    {
        #region Methods

        /// <inheritdoc/>
        public override T PreparePage<T>(T pageObject)
        {
            if (pageObject is HomePage homePage)
            {
                homePage.Load(true);
                return pageObject;
            }
            else
            {
                return base.PreparePage(pageObject);
            }
        }

        #endregion
    }
}
