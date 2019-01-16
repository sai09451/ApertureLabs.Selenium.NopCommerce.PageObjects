using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.EditorSettings
{
    /// <summary>
    /// Used for setting which fields should be shown when the display mode is
    /// advanced.
    /// </summary>
    /// <seealso cref="OpenQA.Selenium.Internal.IWrapsElement" />
    public class SettingGroup : IWrapsElement
    {
        #region Fields

        #region Selectors

        private readonly By titleSelector = By.CssSelector(".panel-heading > .form-group > .col-md-10");
        private readonly By checkAllSelector = By.CssSelector(".panel-heading .select-all-fields");
        private readonly By rowsSelector = By.CssSelector(".panel-body > .form-group");
        private readonly By labelsSelector = By.CssSelector(".panel-body > .form-group .control-label");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingGroup"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        public SettingGroup(IWebElement element)
        {
            WrappedElement = element;
        }

        #endregion

        #region Properties

        #region Elements

        /// <summary>
        /// Gets the <see cref="T:OpenQA.Selenium.IWebElement" /> wrapped by this object.
        /// </summary>
        public IWebElement WrappedElement { get; }

        private IWebElement TitleElement => WrappedElement.FindElement(titleSelector);
        private CheckboxElement CheckAllElement => new CheckboxElement(WrappedElement.FindElement(checkAllSelector));
        private IReadOnlyCollection<IWebElement> LabelElements => WrappedElement.FindElements(labelsSelector);
        private IReadOnlyCollection<IWebElement> RowElements => WrappedElement.FindElements(rowsSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the title of this group.
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return TitleElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets a list of all setting names in this group.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetSettings()
        {
            return RowElements.Select(e => e.TextHelper().InnerText);
        }

        /// <summary>
        /// Sets the value of the setting mathing settingName.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="isBasic">if set to <c>true</c> [is basic].</param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>"
        public SettingGroup SetSetting(string settingName,
            bool isBasic,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var checkbox = GetCheckboxOfSetting(settingName, stringComparison);
            checkbox.Check(isBasic);

            return this;
        }

        /// <summary>
        /// Determines whether the setting is marked as basic.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="stringComparison"></param>
        /// <returns>
        ///   <c>true</c> if [is setting basic] [the specified setting name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSettingBasic(string settingName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var checkbox = GetCheckboxOfSetting(settingName, stringComparison);

            return checkbox.IsChecked;
        }

        /// <summary>
        /// Sets all settings to either basic or advanced.
        /// </summary>
        /// <param name="toBasic">if set to <c>true</c> [to basic].</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SettingGroup SetAllSettings(bool toBasic)
        {
            var checkAllEl = CheckAllElement;

            // If already in the state desired, 'reset' it to avoid errors
            // where checkboxes were being set after calling this. This will
            // always ensure when this method is called ALL of the settings
            // will set.
            if (checkAllEl.IsChecked == toBasic)
                checkAllEl.Check(!toBasic);

            if (toBasic && !checkAllEl.IsChecked)
                checkAllEl.Check(true);
            else if (!toBasic && checkAllEl.IsChecked)
                checkAllEl.Check(false);

            return this;
        }

        private CheckboxElement GetCheckboxOfSetting(string settingName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            // Locate the row element.
            var row = RowElements.FirstOrDefault(e =>
            {
                var labelEl = e.FindElement(By.CssSelector(".control-label"));

                return String.Equals(
                    settingName,
                    labelEl.TextHelper().InnerText,
                    stringComparison);
            });

            // Verify it exists.
            if (row == null)
                throw new NoSuchElementException();

            // Locate the checkbox..
            var checkboxEl = row.FindElement(By.CssSelector(".checkbox"));
            var checkbox = new CheckboxElement(checkboxEl);

            return checkbox;
        }

        #endregion
    }
}
