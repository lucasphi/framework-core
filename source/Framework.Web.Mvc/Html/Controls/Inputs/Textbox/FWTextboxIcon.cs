using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Html.Elements;

namespace Framework.Web.Mvc.Html.Controls.Forms.Textbox
{
    internal sealed class FWTextboxIcon
    {
        public IFWCompositeElement Create(FWInputElement input)
        {
            var iconPlaceholder = new FWDivElement();
            iconPlaceholder.AddCssClass("m-input-icon m-input-icon--left");

            var span = new FWSpanElement();
            span.AddCssClass("m-input-icon__icon m-input-icon__icon--left");

            var innerSpan = new FWSpanElement();
            var i = new FWTagBuilder("i");
            i.AddCssClass(Icon);
            innerSpan.Add(i);

            span.Add(innerSpan);

            iconPlaceholder.Add(span);
            iconPlaceholder.Add(input);

            return iconPlaceholder;
        }

        public FWTextboxIcon(string icon)
        {
            Icon = icon;
        }

        public string Icon { get; private set; }
    }
}
