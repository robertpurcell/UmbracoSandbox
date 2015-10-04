namespace UmbracoSandbox.Web.Controllers.BackOffice
{
    using System;
    using System.Net.Http.Formatting;

    using umbraco.BusinessLogic.Actions;
    using Umbraco.Core;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Trees;

    [Tree("data", "datatree", "Data")]
    [PluginController("Data")]
    public class DataTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string parentId, FormDataCollection queryStrings)
        {
            if (parentId != Constants.System.Root.ToInvariantString())
            {
                throw new NotSupportedException();
            }

            var tree = new TreeNodeCollection
            {
                CreateTreeNode("1", parentId, queryStrings, "Donations", "icon-donate", false)
            };

            return tree;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection
            {
                DefaultMenuAlias = ActionNew.Instance.Alias
            };
            menu.Items.Add<ActionNew>("Custom Action");

            return menu;
        }
    }
}
