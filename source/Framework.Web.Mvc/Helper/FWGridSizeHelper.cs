using Framework.Web.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Helper
{
    static class FWGridSizeHelper
    {
        public const int GRID_COLUMNS = 12;

        public static string GetCss(FWColSize size, FWDevice device)
        {
            return $"col-{GetDeviceName(device)}-{(int)size}";
        }
        
        public static string GetDeviceName(FWDevice device)
        {
            switch(device)
            {
                case FWDevice.Desktop:
                    return "md";
                case FWDevice.Tablet:
                    return "sm";
                case FWDevice.Phone:
                    return "xs";
                case FWDevice.LargeDesktop:
                    return "lg";
            }

            throw new ArgumentException("Invalid device");
        }
    }
}
