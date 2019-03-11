using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Customers
{
    /// <summary>
    /// The model used to create new customers.
    /// </summary>
    public class CustomerCreateModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the customer roles.
        /// </summary>
        /// <value>
        /// The customer roles.
        /// </value>
        public IEnumerable<string> CustomerRoles { get; set; }

        /// <summary>
        /// Gets or sets the manager of vendor.
        /// </summary>
        /// <value>
        /// The manager of vendor.
        /// </value>
        public string ManagerOfVendor { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the admin comment.
        /// </summary>
        /// <value>
        /// The admin comment.
        /// </value>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tax exempt.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is tax exempt; otherwise, <c>false</c>.
        /// </value>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Creates new sletters.
        /// </summary>
        /// <value>
        /// The news letters.
        /// </value>
        public IEnumerable<string> NewsLetters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CustomerCreateModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }
    }
}
