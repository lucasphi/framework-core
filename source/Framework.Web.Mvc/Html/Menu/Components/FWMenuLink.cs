using Framework.Web.Mvc.Html.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Menu.Components
{
    internal class FWMenuLink : IFWMenuComponent
    {
        public string Action { get; set; }

        public string Controller { get; set; }

        public object RouteValues { get; set; }

        public string Icon { get; set; }

        public string Text { get; set; }

        public int Level { get; internal set; }

        public Action<FWAnchorElement> Configure { get; set; }

        public IFWMenuBuilder Build(IFWMenuBuilder builder)
        {
            builder.Item(Text, Action, Controller, RouteValues, Icon, Configure);
            return null;
        }
    }
}
