using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Menu.Components
{
    internal class FWMenuSeparator : IFWMenuComponent
    {
        public int Level { get; internal set; }

        public IFWMenuBuilder Build(IFWMenuBuilder builder)
        {
            builder.Separator();
            return null;
        }
    }
}
