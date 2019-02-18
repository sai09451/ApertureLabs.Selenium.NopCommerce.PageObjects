using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar
{
    /// <summary>
    /// A node present in the admin main sidebar.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.IPageComponent" />
    public interface IAdminMainSideBarNode : IFluidPageComponent<IAdminMainSideBarNode>
    {
        /// <summary>
        /// Gets the child items of this node.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAdminMainSideBarNode> GetItems();

        /// <summary>
        /// Tries to expand the node.
        /// </summary>
        /// <returns></returns>
        IAdminMainSideBarNode Expand();

        /// <summary>
        /// Tries to collapses the node.
        /// </summary>
        /// <returns></returns>
        IAdminMainSideBarNode Collapse();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Gets the icon class.
        /// </summary>
        /// <returns></returns>
        string GetIcon();

        /// <summary>
        /// Goes to page. Will throw an exception if the node doesn't point to
        /// anything.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="UriFormatException">
        /// Thrown if the elements href attribute doesn't point to a url.
        /// </exception>
        T Select<T>() where T : IPageObject;
    }
}
