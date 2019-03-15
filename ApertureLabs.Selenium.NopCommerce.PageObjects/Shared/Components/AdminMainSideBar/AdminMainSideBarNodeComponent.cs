using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar
{
    /// <summary>
    /// AdminmainSideBarNode.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar.IAdminMainSideBarNodeComponent" />
    public class AdminMainSideBarNodeComponent : FluidPageComponent<IAdminMainSideBarNodeComponent>,
        IAdminMainSideBarNodeComponent
    {
        #region Fields

        #region Selectors

        private readonly By subMenuSelector = By.CssSelector(".treeview-menu");
        private readonly By nodeTitleSelector = By.CssSelector("a");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminMainSideBarNodeComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public AdminMainSideBarNodeComponent(By selector,
            IAdminMainSideBarNodeComponent parent,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver, parent)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        private bool CanBeExpanded { get; set; }

        #region Elements

        private IWebElement SubMenuElement => WrappedElement
            .FindElement(subMenuSelector);

        private IWebElement NodeTitleElement => WrappedElement
            .FindElement(nodeTitleSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// If overloaded don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();

            CanBeExpanded = WrappedElement.Classes().Contains("treeview");

            return this;
        }

        /// <summary>
        /// Tries to collapses the node.
        /// </summary>
        /// <returns></returns>
        public virtual IAdminMainSideBarNodeComponent Collapse()
        {
            if (CanBeExpanded && IsExpanded())
            {
                WrappedElement.Click();

                // Wait until the sub-menu is hidden.
                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(2))
                    .Until(d => !SubMenuElement.Displayed);
            }

            return this;
        }

        /// <summary>
        /// Tries to expand the node.
        /// </summary>
        /// <returns></returns>
        public virtual IAdminMainSideBarNodeComponent Expand()
        {
            if (CanBeExpanded && !IsExpanded())
            {
                WrappedElement.Click();

                // Wait until the sub-menu is hidden.
                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(2))
                    .Until(d => SubMenuElement.Displayed);
            }

            return this;
        }

        /// <summary>
        /// Gets the icon class.
        /// </summary>
        /// <returns></returns>
        public virtual string GetIcon()
        {
            var firstChildEl = NodeTitleElement.Children().FirstOrDefault();

            if (firstChildEl?.TagName == "i")
            {
                var classIcon = firstChildEl.Classes()
                    .Except(new[] { "fa" })
                    .FirstOrDefault();

                return classIcon;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the child items of this node.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<IAdminMainSideBarNodeComponent> GetItems()
        {
            foreach (var el in SubMenuElement.Children())
            {
                yield return pageObjectFactory.PrepareComponent(
                    new AdminMainSideBarNodeComponent(
                        ByElement.FromElement(el),
                        this,
                        pageObjectFactory,
                        WrappedDriver));
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return NodeTitleElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Goes to page. Will throw an exception if the node doesn't point to
        /// anything.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T Select<T>() where T : IPageObject
        {
            if (CanBeExpanded)
                throw new UriFormatException();

            ExpandParents();
            NodeTitleElement.Click();

            return pageObjectFactory.PreparePage<T>();
        }

        private bool IsExpanded()
        {
            return WrappedElement.Classes().Contains("menu-open");
        }

        private void ExpandParents()
        {
            var currentParent = Parent();

            while (currentParent != null)
            {
                currentParent.Expand();
                currentParent = currentParent.Parent();
            }
        }

        #endregion

    }
}
