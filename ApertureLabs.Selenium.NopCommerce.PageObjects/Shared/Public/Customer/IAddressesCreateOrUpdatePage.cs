using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// Base interface for the IAddressesAdd and IAddressesEdit interfaces.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IHasAccountNavigation{T}" />
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.IViewModel{T}" />
    public interface IAddressesCreateOrUpdatePage<T> : IBasePage,
        IHasAccountNavigation<T>,
        IViewModel<AddressModel>
        where T : IPageObject
    {
        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <returns></returns>
        string GetFirstName();

        /// <summary>
        /// Sets the first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetFirstName(string firstName);

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <returns></returns>
        string GetLastName();

        /// <summary>
        /// Sets the last name.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetLastName(string lastName);

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        string GetEmail();

        /// <summary>
        /// Sets the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetEmail(string email);

        /// <summary>
        /// Gets the company.
        /// </summary>
        /// <returns></returns>
        string GetCompany();

        /// <summary>
        /// Sets the company.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetCompany(string company);

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <returns></returns>
        string GetCountry();

        /// <summary>
        /// Sets the country.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetCountry(string country);

        /// <summary>
        /// Gets the state province.
        /// </summary>
        /// <returns></returns>
        string GetStateProvince();

        /// <summary>
        /// Sets the state province.
        /// </summary>
        /// <param name="stateProvince">The state province.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetStateProvince(string stateProvince);

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <returns></returns>
        string GetCity();

        /// <summary>
        /// Sets the city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetCity(string city);

        /// <summary>
        /// Gets the address1.
        /// </summary>
        /// <returns></returns>
        string GetAddress1();

        /// <summary>
        /// Sets the address1.
        /// </summary>
        /// <param name="address1">The address1.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetAddress1(string address1);

        /// <summary>
        /// Gets the address2.
        /// </summary>
        /// <returns></returns>
        string GetAddress2();

        /// <summary>
        /// Sets the address2.
        /// </summary>
        /// <param name="address2">The address2.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetAddress2(string address2);

        /// <summary>
        /// Gets the zip code.
        /// </summary>
        /// <returns></returns>
        string GetZipCode();

        /// <summary>
        /// Sets the zip code.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetZipCode(string zipCode);

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <returns></returns>
        string GetPhoneNumber();

        /// <summary>
        /// Sets the phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetPhoneNumber(string phoneNumber);

        /// <summary>
        /// Gets the fax number.
        /// </summary>
        /// <returns></returns>
        string GetFaxNumber();

        /// <summary>
        /// Sets the fax number.
        /// </summary>
        /// <param name="faxNumber">The fax number.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetFaxNumber(string faxNumber);

        /// <summary>
        /// Sets from model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        IAddressesCreateOrUpdatePage<T> SetFromModel(AddressModel model);

        /// <summary>
        /// Saves the address.
        /// </summary>
        /// <returns></returns>
        IAddressesPage Save();
    }
}
