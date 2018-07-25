using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls.Inputs.Detail
{
    internal static class FWDetailHelper
    {
        public static FWButtonControl CreateUndoButton()
        {
            FWButtonControl button = new FWButtonControl()
                                            .Icon("fa fa-undo")                                            
                                            .Behavior("Undo");
            button.Text = ViewResources.Btn_UndoRemoved;
            button.Size(FWElementSize.Small);
            button.Title = ViewResources.Btn_UndoRemoved;
            button.Attributes.Add("style", "display:none");
            return button;
        }

        public static FWButtonControl CreateAddButton()
        {
            FWButtonControl button = new FWButtonControl()
                                            .Icon("fa fa-plus")   
                                            .Behavior(FWButtonBehavior.Add);
            button.Title = ViewResources.Btn_Add;
            button.AddCssClass("btn-add");
            return button;
        }

        public static FWButtonControl CreateRemoveButton()
        {
            FWButtonControl button = new FWButtonControl()
                                            .Behavior(FWButtonBehavior.Exclude);
            button.AddCssClass("btn-detail");
            return button;
        }
    }
}
