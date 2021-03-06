using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Text;
using TeamWayPath.Feature.SuperScripts.Models;

namespace TeamWayPath.Feature.SuperScripts.Controllers
{
    public class SuperScriptsController : Controller
    {
        // GET: SuperScripts
        // Builds left hand menu of scripts
        public ActionResult TreeMenu()
        {
            var coreDb = Sitecore.Configuration.Factory.GetDatabase("core");
            var dataSourceId = RenderingContext.CurrentOrNull.Rendering.DataSource;
            var dataSource = coreDb.GetItem(dataSourceId);

            if (dataSource == null)
            {
                return View(model: null);
            }

            //get datasource fields
            var rootItem = dataSource["RootItemID"];
            var databaseToUse = dataSource["Database"];

            if (string.IsNullOrEmpty(rootItem) || string.IsNullOrEmpty(databaseToUse))
            {
                return View(model: null);
            }

            var db = Sitecore.Configuration.Factory.GetDatabase(databaseToUse) ?? coreDb;
            var root = db.GetItem(rootItem);

            if (root == null)
            {
                return View(model: null);
            }

            var model = new List<TreeMenuViewModel>();

            foreach (Item item in root.Children)
            {
                var treeMenuItem = new TreeMenuViewModel();
                treeMenuItem.Title = item.DisplayName;

                var menuItems = new List<MenuItem>();
                foreach (Item child in item.Children)
                {
                    var menuItem = new MenuItem()
                    {
                        Title = child.DisplayName,
                        Link = "?scriptID=" + child.ID.ToString()
                    };
                    menuItems.Add(menuItem);
                }

                treeMenuItem.MenuItems = menuItems;
                model.Add(treeMenuItem);
            }

            return View(model);


        }

        // Builds display and action button for each script
        public ActionResult DisplayScript()
        {
            string scriptID = Request.QueryString["scriptID"];

            // no script request, send to main
            if (String.IsNullOrEmpty(scriptID))
                return View("MainMenu");


            var masterDb = Sitecore.Configuration.Factory.GetDatabase("master");

            var scriptFolderItem = masterDb.GetItem(scriptID);

            // item not found, send error
            if (scriptFolderItem == null)
                return View("Error");

            var model = new DisplayScriptViewModel();

            var scriptItems = new List<ScriptItem>();
            foreach (Item child in scriptFolderItem.Children)
            {
                var urlString = new UrlString(UIUtil.GetUri("control:PowerShellRunner"));
                urlString.Append("scriptId", child.ID.ToString());
                urlString.Append("scriptDb", "master");

                scriptItems.Add(new ScriptItem()
                {
                    Title = child.DisplayName,
                    Script = child["Script"],
                    Link = urlString.ToString()
                });
            }

            model.ScriptItems = scriptItems;

            return View("DisplayScript", model);
        }
    }
}