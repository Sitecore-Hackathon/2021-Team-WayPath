using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamWayPath.Feature.SuperScripts.Models
{
    public class TreeMenuViewModel
    {
        public string Title { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}