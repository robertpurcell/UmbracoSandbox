namespace UmbracoSandbox.Web.Controllers.BackOffice
{
    using System;
    using System.Net.Http.Formatting;
    using umbraco.BusinessLogic.Actions;
    using Umbraco.Core;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Trees;

    [Tree("data", "dataTree", "Data", iconClosed: "icon-doc")]
    [PluginController("Data")]
    public class DataTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            //check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                var tree = new TreeNodeCollection
                {
                    CreateTreeNode("1", id, queryStrings, "Donations", "icon-coin-pound", false)
                };

                return tree;
            }
            //this tree doesn't suport rendering more than 1 level
            throw new NotSupportedException();
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            menu.DefaultMenuAlias = ActionNew.Instance.Alias;

            return menu;
        }
    }
}
