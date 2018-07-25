using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Menu.Components
{
    internal class FWMenuLabel : IFWMenuComponent
    {
        public string Text { get; set; }

        public int Level { get; internal set; }

        public IFWMenuBuilder Build(IFWMenuBuilder builder)
        {
            builder.Label(Text);
            return null;
        }
    }
}
