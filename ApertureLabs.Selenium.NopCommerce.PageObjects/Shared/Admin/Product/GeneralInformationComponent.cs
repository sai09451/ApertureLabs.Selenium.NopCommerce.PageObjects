using ApertureLabs.Selenium.Components.TinyMCE;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.Components.JQuery.TagEditor;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ApertureLabs.Selenium.Extensions;
using System.Collections;
using System.Collections.Generic;
using ApertureLabs.Selenium.WebElements.Option;
using ApertureLabs.Selenium.Components.Kendo.KDatePicker;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The 'General Info' widget on the 'Info' tab.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class GeneralInformationComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By idSelector = By.CssSelector(".panel-body > div:nth-child(1) > div.col-md-9 > div");
        private readonly By productTypeSelector = By.CssSelector("#ProductTypeId");
        private readonly By visibleIndividuallySelector = By.CssSelector("#VisibleIndividually");
        private readonly By productNameSelector = By.CssSelector("#Name");
        private readonly By shortDescriptionSelector = By.CssSelector("#ShortDescription");
        private readonly By fullDescriptionSelector = By.CssSelector("#FullDescription");
        private readonly By skuSelector = By.CssSelector("#Sku");
        private readonly By publishedSelector = By.CssSelector("#Published");
        private readonly By productTagsSelector = By.CssSelector("#ProductTags");
        private readonly By gtinSelector = By.CssSelector("#Gtin");
        private readonly By manufacturerPartNumberSelector = By.CssSelector("#ManufacturerPartNumber");
        private readonly By showOnHomePageSelector = By.CssSelector("#ShowOnHomePage");
        private readonly By displayOrderSelector = By.CssSelector("#DisplayOrder");
        private readonly By allowCustomerReviewsSelector = By.CssSelector("#AllowCustomerReviews");
        private readonly By availableStartDateSelector = By.CssSelector("#AvailableStartDateTimeUtc");
        private readonly By availableEndDateSelector = By.CssSelector("#AvailableEndDateTimeUtc");
        private readonly By markAsNewSelector = By.CssSelector("#MarkAsNew");
        private readonly By adminCommentSelector = By.CssSelector("#AdminComment");
        private readonly By createdOnSelector = By.CssSelector("div > div.col-md-7 > div > div:nth-child(1) > div.panel-body > div:nth-child(20) > div.col-md-9 > div");
        private readonly By updatedOnSelector = By.CssSelector("div > div.col-md-7 > div > div:nth-child(1) > div.panel-body > div:nth-child(21) > div.col-md-9 > div");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralInformationComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public GeneralInformationComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            FullDescriptionComponent = new TinyMCEComponent(
                fullDescriptionSelector,
                pageObjectFactory,
                WrappedDriver,
                new TinyMCEOptions());

            ProductTagsComponent = new TagEditorComponent<GeneralInformationComponent>(
                productTagsSelector,
                WrappedDriver,
                this,
                new TagEditorConfiguration());

            AvailableEndDateComponent = new KDatePickerComponent<GeneralInformationComponent>(
                new KDatePickerConfiguration(),
                availableEndDateSelector,
                WrappedDriver,
                this);

            AvailableStartDateComponent = new KDatePickerComponent<GeneralInformationComponent>(
                new KDatePickerConfiguration(),
                availableStartDateSelector,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement IdElement => WrappedElement
            .FindElement(idSelector);

        private SelectElement ProductTypeElement => new SelectElement(
            WrappedElement.FindElement(
                productTypeSelector));

        private CheckboxElement VisibleIndividuallyElement => new CheckboxElement(
            WrappedElement.FindElement(
                visibleIndividuallySelector));

        private InputElement ProductNameElement => new InputElement(
            WrappedElement.FindElement(
                productNameSelector));

        private InputElement ShortDescriptionElement => new InputElement(
            WrappedElement.FindElement(
                shortDescriptionSelector));

        private InputElement SkuElement => new InputElement(
            WrappedElement.FindElement(
                skuSelector));

        private CheckboxElement PublishedElement => new CheckboxElement(
            WrappedElement.FindElement(
                publishedSelector));

        private InputElement GTINElement => new InputElement(
            WrappedElement.FindElement(
                gtinSelector));

        private InputElement ManufacturerPartNumberElement => new InputElement(
            WrappedElement.FindElement(
                manufacturerPartNumberSelector));

        private CheckboxElement ShowOnHomePageElement => new CheckboxElement(
            WrappedElement.FindElement(
                showOnHomePageSelector));

        private InputElement DisplayOrderElement => new InputElement(
            WrappedElement.FindElement(
                displayOrderSelector));

        private CheckboxElement AllowCustomerReviewsElement => new CheckboxElement(
            WrappedElement.FindElement(
                allowCustomerReviewsSelector));

        private CheckboxElement MarkAsNewElement => new CheckboxElement(
            WrappedElement.FindElement(
                markAsNewSelector));

        private InputElement AdminCommentElement => new InputElement(
            WrappedElement.FindElement(
                adminCommentSelector));

        private IWebElement CreatedOnElement => WrappedElement
            .FindElement(createdOnSelector);

        private IWebElement UpdatedOnElement => WrappedElement
            .FindElement(updatedOnSelector);

        #endregion

        private TinyMCEComponent FullDescriptionComponent { get; }

        private TagEditorComponent<GeneralInformationComponent> ProductTagsComponent { get; }

        private KDatePickerComponent<GeneralInformationComponent> AvailableStartDateComponent { get; }

        private KDatePickerComponent<GeneralInformationComponent> AvailableEndDateComponent { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the product id.
        /// </summary>
        /// <returns></returns>
        public virtual int GetId()
        {
            return IdElement.TextHelper().ExtractInteger();
        }

        /// <summary>
        /// Gets the type of the product.
        /// </summary>
        /// <returns></returns>
        public virtual string GetProductType()
        {
            return ProductTypeElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the available product types.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableProductTypes()
        {
            foreach (var option in ProductTypeElement.Options)
                yield return option.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the type of the product.
        /// </summary>
        /// <param name="productType">Type of the product.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException">
        /// No option found matching that productType.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// productType cannot be null or empty.
        /// </exception>
        public virtual GeneralInformationComponent SetProductType(
            string productType,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (String.IsNullOrEmpty(productType))
            {
                throw new ArgumentException($"{nameof(productType)} cannot " +
                    $"be null or empty.");
            }

            var index = ProductTypeElement.Options.IndexOf(
                el => String.Equals(
                    el.TextHelper().InnerText,
                    productType,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            ProductTypeElement.SelectByIndex(index);

            return this;
        }

        /// <summary>
        /// Gets the value of visible individually.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetVisibleIndividually()
        {
            return VisibleIndividuallyElement.IsChecked;
        }

        /// <summary>
        /// Sets the visible individually.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetVisibleIndividually(bool visible)
        {
            VisibleIndividuallyElement.Check(visible);

            return this;
        }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <returns></returns>
        public virtual string GetProductName()
        {
            return ProductNameElement.GetValue<string>();
        }

        /// <summary>
        /// Sets the name of the product.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetProductName(string productName)
        {
            ProductNameElement.SetValue(productName);

            return this;
        }

        /// <summary>
        /// Gets the short description.
        /// </summary>
        /// <returns></returns>
        public virtual string GetShortDescription()
        {
            return ShortDescriptionElement.GetValue<string>();
        }

        /// <summary>
        /// Sets the short description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetShortDescription(string description)
        {
            ShortDescriptionElement.SetValue(description);

            return this;
        }

        /// <summary>
        /// Gets the full description.
        /// </summary>
        /// <returns></returns>
        public virtual string GetFullDescription()
        {
            return FullDescriptionComponent.GetContent();
        }

        /// <summary>
        /// Sets the full description.
        /// </summary>
        /// <param name="fullDescription">The full description.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetFullDescription(string fullDescription)
        {
            FullDescriptionComponent.ClearAllContent();
            FullDescriptionComponent.WriteLine(fullDescription);

            return this;
        }

        /// <summary>
        /// Gets the sku.
        /// </summary>
        /// <returns></returns>
        public virtual string GetSku()
        {
            return SkuElement.GetValue<string>();
        }

        /// <summary>
        /// Sets the sku.
        /// </summary>
        /// <param name="sku">The sku.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetSku(string sku)
        {
            SkuElement.SetValue(sku);

            return this;
        }

        /// <summary>
        /// Checks if the product is marked as published.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetPublished()
        {
            return PublishedElement.IsChecked;
        }

        /// <summary>
        /// Sets the published.
        /// </summary>
        /// <param name="published">if set to <c>true</c> [published].</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetPublished(bool published)
        {
            PublishedElement.Check(published);

            return this;
        }

        /// <summary>
        /// Gets the global trade item number.
        /// </summary>
        /// <returns></returns>
        public virtual string GetGTIN()
        {
            return GTINElement.GetValue<string>();
        }

        /// <summary>
        /// Sets the global trade item number.
        /// </summary>
        /// <param name="gtin">The gtin.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetGTIN(string gtin)
        {
            GTINElement.SetValue(gtin);

            return this;
        }

        /// <summary>
        /// Gets the manufacturer part number.
        /// </summary>
        /// <returns></returns>
        public virtual string GetManufacturerPartNumber()
        {
            return ManufacturerPartNumberElement.GetValue<string>();
        }

        /// <summary>
        /// Sets the manufacturer part number.
        /// </summary>
        /// <param name="partNumber">The part number.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetManufacturerPartNumber(string partNumber)
        {
            ManufacturerPartNumberElement.SetValue(partNumber);

            return this;
        }

        /// <summary>
        /// Gets the value of if the product is shown on home page.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetShowOnHomePage()
        {
            return ShowOnHomePageElement.IsChecked;
        }

        /// <summary>
        /// Sets the value of if the product is shown on home page.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetShowOnHomePage(bool show)
        {
            ShowOnHomePageElement.Check(show);

            return this;
        }

        /// <summary>
        /// Gets the display order.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidElementStateException">In order to use " +
        ///                     "this the product must be shown on the homepage.</exception>
        public virtual int GetDisplayOrder()
        {
            if (!GetShowOnHomePage())
            {
                throw new InvalidElementStateException("In order to use " +
                    "this the product must be shown on the homepage.");
            }

            return DisplayOrderElement.GetValue<int>();
        }

        /// <summary>
        /// Sets the display order.
        /// </summary>
        /// <param name="displayOrder">The display order.</param>
        /// <returns></returns>
        /// <exception cref="InvalidElementStateException">
        /// In order to use this the product must be shown on the homepage.
        /// </exception>
        public virtual GeneralInformationComponent SetDisplayOrder(int displayOrder)
        {
            if (!GetShowOnHomePage())
            {
                throw new InvalidElementStateException("In order to use " +
                    "this the product must be shown on the homepage.");
            }

            DisplayOrderElement.SetValue(displayOrder);

            return this;
        }

        /// <summary>
        /// Gets the allow customer reviews.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetAllowCustomerReviews()
        {
            return AllowCustomerReviewsElement.IsChecked;
        }

        /// <summary>
        /// Sets the allow customer reviews.
        /// </summary>
        /// <param name="allowReviews">if set to <c>true</c> [allow reviews].</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetAllowCustomerReviews(bool allowReviews)
        {
            AllowCustomerReviewsElement.Check(allowReviews);

            return this;
        }

        /// <summary>
        /// Gets the available start date.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime? GetAvailableStartDate()
        {
            return AvailableStartDateComponent.GetValue();
        }

        /// <summary>
        /// Sets the available start date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetAvailableStartDate(DateTime? startDate)
        {
            AvailableStartDateComponent.SetValue(startDate);

            return this;
        }

        /// <summary>
        /// Gets the available end date.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime? GetAvailableEndDate()
        {
            return AvailableEndDateComponent.GetValue();
        }

        /// <summary>
        /// Sets the available end date.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetAvailableEndDate(DateTime? endDate)
        {
            AvailableEndDateComponent.SetValue(endDate);

            return this;
        }

        /// <summary>
        /// Gets the mark as new.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetMarkAsNew()
        {
            return MarkAsNewElement.IsChecked;
        }

        /// <summary>
        /// Sets the mark as new.
        /// </summary>
        /// <param name="markAsNew">if set to <c>true</c> [mark as new].</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetMarkAsNew(bool markAsNew)
        {
            MarkAsNewElement.Check(markAsNew);

            return this;
        }

        /// <summary>
        /// Gets the admin comment.
        /// </summary>
        /// <returns></returns>
        public virtual string GetAdminComment()
        {
            return AdminCommentElement.GetValue<string>();
        }

        /// <summary>
        /// Sets the admin comment.
        /// </summary>
        /// <param name="adminComment">The admin comment.</param>
        /// <returns></returns>
        public virtual GeneralInformationComponent SetAdminComment(string adminComment)
        {
            AdminCommentElement.SetValue(adminComment);

            return this;
        }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetCreatedOn()
        {
            return CreatedOnElement
                .TextHelper()
                .ExtractDateTime("dddd, MMMM d, yyyy h:m:ss tt");
        }

        /// <summary>
        /// Gets the updated on.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetUpdatedOn()
        {
            return UpdatedOnElement
                .TextHelper()
                .ExtractDateTime("dddd, MMMM d, yyyy h:m:ss tt");
        }

        #endregion
    }
}
