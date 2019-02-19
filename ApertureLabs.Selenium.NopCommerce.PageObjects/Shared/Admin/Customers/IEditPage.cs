namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// The admin customer edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IBasePage" />
    public interface IEditPage : IBasePage
    {
        /// <summary>
        /// Returns to the customer list.
        /// </summary>
        /// <returns></returns>
        IListPage BackToCustomerList();

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <returns></returns>
        IListPage Delete();

        /// <summary>
        /// Saves the customer and returns to the customer list.
        /// </summary>
        /// <returns></returns>
        IListPage Save();

        /// <summary>
        /// Saves the customer and continues to edit.
        /// </summary>
        /// <returns></returns>
        IEditPage SaveAndContinueEdit();
    }
}
