using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using Microsoft.Extensions.DependencyInjection;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Factories
{
    /// <summary>
    /// CustomPageObjectFactory.
    /// </summary>
    public class CustomPageObjectFactory : PageObjectFactory
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageObjectFactory"/> class.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public CustomPageObjectFactory(IServiceCollection serviceCollection)
            : base(serviceCollection, true)
        { }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override T PreparePage<T>()
        {
            var pageObject = base.PreparePage<T>();

            if (pageObject is HomePage homePage)
            {
                homePage.Load(true);
                return pageObject;
            }
            else
            {
                return base.PreparePage<T>();
            }
        }

        #endregion
    }
}
