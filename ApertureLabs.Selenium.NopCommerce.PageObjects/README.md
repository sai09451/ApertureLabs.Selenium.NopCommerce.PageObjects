# README

## Purpose
This project is to recreate what a user (admin, normal, guest) sees when they
visit the site in the form of PageObjects.

## Definitions
* PageObject - A class that represents a webpage.
* PageComponent - A class that represents either a view component or a partial
	view.
* WebDriver - A class that controls a browser.
* WebElement - A class that represents an element.

## General Notes
* Suffix all PageObjects with 'Page'.
* Suffix all PageComponents with 'Component'.
* Standard template for a PageObject:
	public class YourPageObject : YourBasePageObject [(Optional), IViewModel<YourModel>]
	{
		#region Fields

		#region Selectors

		private readonly By emailInputSelector = By.CssSelector(".email");
		private readonly By passwordInputSelector = By.CssSelector(".password");
		private readonly By submitButtonSelector = By.CssSelector(".submit");

		#endregion

		// Other fields would go here

		#endregion

		#region Constructor(s)

		public YourPageObject(IWebDriver driver, YourPageSettingsClass settings)
			: base(driver, settings)
		{ }

		#endregion

		#region Properties

		#region Elements

		private IWebElement EmailInputElement => WrappedDriver.FindElement(emailInputSelector);
		private IWebElement PasswordInputElement => WrappedDriver.FindElement(passwordInputSelector);
		private IWebElement SubmitButtonElement => WrappedDriver.FindElement(submitButtonSelector);

		#endregion

		// Only include if you implement the IViewModel<T> interface
		public YourModel ViewModel
		{
			get
			{
				// This example pretends the model has two properties: Email and Password.
				var model = new YourModel
				{
					Email = EmailInputElement.GetAttribute("value"),
					Password = PasswordInputElement.GetAttribute("value")
				};

				return model;
			}
		}

		// Other Properties would go here

		#endregion

		#region Methods

		// Optionally override Load()
		public override ILoadableComponent Load()
		{
			// Make sure base.Load() is called.
			base.Load();

			// Any other intialization is done here.

			return this;
		}

		// Optionally override IsStale()
		public override bool IsStale()
		{
			// Make sure to call base.IsStale() first.
			if (base.IsStale())
				return true;

			// Your custom logic.
			var isStale = false;
			return isStale;
		}

		// Other Methods

		// Example implementation of a Login(...) method.
		public LoginResultPage Login(string email, string password)
		{
			EmailInputElement.SendKeys(email);
			PasswordInputElement.SendKeys(password);
			SubmitButtonElement.Click();

			var resultPage = new LoginResultPage(WrappedDriver, PageSettings);
			
			// Don't forget to call Load() when instantiating new PageObjects.
			resultPage.Load(); 
			
			return resultPage;

			// Could be rewritten as:
			// return new LoginResultPage(WrappedDriver, PageSettings).Load() as LoginPageResult;
		}

		#endregion
	}
* This template makes heavy use of regions as in my experience the selectors
	and elements can often get out of hand and regions help keep the two
	organized.
* If a PageObject has a lot of Selectors/Elements, either create a new
	PageComponent which represents a portion of the page or move
	Selector/Element declarations that are only specific to certian
	methods/properties to those methods/properties.
* Try not to expose the elements in the PageObjects, just expose the actions of
	that page. Example: A LoginPage class would have the following
	properties: emailInputElement, passwordInputElement, and a
	submitButtonElement. These should be private. The class should then
	have a public method named 'Login' that would handle entering the
	information and clicking the submit button.
* If a PageObject class starts growing too large, break up it's functionality
	into PageComponents (but keep those components in the same folder).

## Organization
The top level folders should include all supported versions of Nop and a
'shared' folder for files common to multiple versions of Nop.

Each root version folder are broken down into four folders:
	1. Public
	2. Admin
	3. Components
	4. Resources

### Public
The files here are used to represent all of the 'Public' views accross ALL of
the plugins. Try and mimic the same folder structure as the Nop.Web > Views
folder.

### Admin
Pretty much the same thing as the Public folder but for the admin pages. Again
try and mimic the folder structure of Nop.Web > Areas > Admin > Views.

### Components
This is where you mock up the view components. These classes should all implement
IPageComponent. There is no distinction between admin and public components,
they all go here. This is also the place to define custom elements. NOTE: This
isn't where 'partial' views (unless they're used in multiple files) go, those
should be placed in alongside their parent file counter parts.

### Resources
This is where your utility/service classes go. Usually this contains the
following sub-folders if its contents grow enough:
	1. Exceptions - Contains custom excptions.
	2. Extensions - Contains extension methods.
	3. Factories - Contains factories for instantiating Services/Models.
	4. Models - Contains models.
	5. Services - Contains service classes.
	6. Utilities - Static helper classes & functions.

The models folder should not contain identical models to the referenced
project(s) being mocked, try and use those models when possible.
