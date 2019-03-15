using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The 'Rental' component on the info tab of the admin product edit page.
    /// </summary>
    public class RentalComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By rentalProductSelector = By.CssSelector("#IsRental");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public RentalComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement RentalProductElement => new CheckboxElement(
            WrappedElement.FindElement(
                rentalProductSelector));

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance is rental.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is rental; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool GetIsRental()
        {
            return RentalProductElement.IsChecked;
        }

        /// <summary>
        /// Sets the is rental.
        /// </summary>
        /// <param name="isRental">if set to <c>true</c> [is rental].</param>
        /// <returns></returns>
        public virtual RentalComponent SetIsRental(bool isRental)
        {
            RentalProductElement.Check(isRental);

            return this;
        }

        #endregion
    }
}
