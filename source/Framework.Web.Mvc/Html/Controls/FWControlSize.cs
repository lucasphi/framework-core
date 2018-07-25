using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    struct FWControlSize
    {
        public FWControlSize(FWColSize size, FWDevice device)
        {
            Size = size;
            Device = device;
        }

        public FWColSize Size { get; set; }

        public FWDevice Device { get; set; }
    }
}
