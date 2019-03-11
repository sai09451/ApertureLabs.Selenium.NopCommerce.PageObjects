using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Customers;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// The admin customer create page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IBasePage" />
    public interface ICreatePage : IBasePage
    {
        /// <summary>
        /// Cancels creating the customer and returns to the customer list.
        /// </summary>
        /// <returns></returns>
        IListPage BackToCustomerList();

        /// <summary>
        /// Enters the inforamation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ICreatePage EnterInformation(CustomerCreateModel model);

        /// <summary>
        /// Saves the customer and returns to the list page.
        /// </summary>
        /// <returns></returns>
        IListPage Save();

        /// <summary>
        /// Saves the customer and continues to edit them on the
        /// <see cref="IEditPage"/>.
        /// </summary>
        /// <returns></returns>
        IEditPage SaveAndContinue();
    }
}
