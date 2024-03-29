﻿using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminAjax;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.EditorSettings
{
    /// <summary>
    /// The mode displays/hides advanced editor fields.
    /// </summary>
    public enum EditorMode
    {
        /// <summary>
        /// Advanced fields are hidden.
        /// </summary>
        Basic = 0,

        /// <summary>
        /// Advanced fields are displayed.
        /// </summary>
        Advanced = 1
    };

    /// <summary>
    /// The 'Settings' button available on most editor pages which will
    /// determine which fields will be displayed/hidden when the view is
    /// switched between 'Basic' and 'Advanced'.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminAjax.AdminAjaxComponent" />
    public class EditorSettingsComponent : AdminAjaxComponent
    {
        #region Fields

        private readonly EditorSettingsComponentConfiguration configuration;

        #region Selectors

        private readonly By closeModalSelector = By.CssSelector(".close");
        private readonly By advancedButtonSelector;
        private readonly By advancedCheckboxSelector = By.CssSelector("#advanced-settings-mode");
        private readonly By settingsButtonSelector;
        private readonly By settingPanelGroupsSelector = By.CssSelector(".panel.panel-default");

        private By modalSelector;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorSettingsComponent"/> class.
        /// </summary>
        /// <param name="advancedButtonSelector">
        /// The 'Advanced' button selector.
        /// </param>
        /// <param name="settingsButtonSelector">
        /// The 'Settings' button selector
        /// </param>
        /// <param name="configuration"></param>
        /// <param name="driver">The driver.</param>
        public EditorSettingsComponent(By advancedButtonSelector,
            By settingsButtonSelector,
            EditorSettingsComponentConfiguration configuration,
            IWebDriver driver)
            : base(advancedButtonSelector, driver)
        {
            this.advancedButtonSelector = advancedButtonSelector
                ?? throw new ArgumentNullException(nameof(advancedButtonSelector));
            this.settingsButtonSelector = settingsButtonSelector
                ?? throw new ArgumentNullException(nameof(settingsButtonSelector));
            this.configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AdvancedButtonElement => WrappedDriver.FindElement(advancedButtonSelector);
        private CheckboxElement AdvancedCheckboxElement => new CheckboxElement(AdvancedButtonElement.FindElement(advancedCheckboxSelector));
        private IWebElement SettingsButtonElement => WrappedDriver.FindElement(settingsButtonSelector);
        private IWebElement ModalElement => WrappedDriver.FindElement(modalSelector);
        private IWebElement CloseModalElement => ModalElement.FindElement(closeModalSelector);

        private IReadOnlyCollection<SettingGroup> SettingPanelGroupElements => ModalElement.FindElements(settingPanelGroupsSelector)
            .Select(e => new SettingGroup(e))
            .ToList()
            .AsReadOnly();

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// If overriding don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();

            var dataTarget = SettingsButtonElement.GetAttribute("data-target");
            modalSelector = By.CssSelector(dataTarget);

            return this;
        }

        /// <summary>
        /// Gets the mode ("Basic" or "Advanced").
        /// </summary>
        /// <returns>Basic or Advanced.</returns>
        public virtual EditorMode GetMode()
        {
            return AdvancedCheckboxElement.IsChecked
                ? EditorMode.Advanced
                : EditorMode.Basic;
        }

        /// <summary>
        /// Sets the mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual EditorSettingsComponent SetMode(EditorMode mode,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (GetMode() != mode)
                AdvancedCheckboxElement.Click();

            // Wait for ajax loader.
            WaitForAjaxBusy(TimeSpan.FromMinutes(1));

            return this;
        }

        /// <summary>
        /// Opens the 'Settings' button and executes the passed in function
        /// with all of the listed setting panel groups.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public virtual EditorSettingsComponent Settings(
            Action<IEnumerable<SettingGroup>> action)
        {
            OpenModal();
            action(SettingPanelGroupElements);
            CloseModal();

            return this;
        }

        private bool IsModalOpen()
        {
            return ModalElement.Displayed;
        }

        private void OpenModal()
        {
            if (!IsModalOpen())
            {
                SettingsButtonElement.Click();
            }
        }

        private void CloseModal()
        {
            if (IsModalOpen())
            {
                if (configuration.UseKeyboardInsteadOfMouseToInteract)
                    ModalElement.SendKeys(Keys.Escape);
                else
                    CloseModalElement.Click();
            }
        }

        #endregion
    }
}
