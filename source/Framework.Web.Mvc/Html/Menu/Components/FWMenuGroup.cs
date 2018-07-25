using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Menu.Components
{
    internal class FWMenuGroup : IFWMenuComponent
    {
        public string Icon { get; set; }

        public string Text { get; set; }

        public int Level { get; internal set; }

        public IFWMenuBuilder Build(IFWMenuBuilder builder)
        {
            return builder.Holder(Text, Icon);
        }
    }
}
