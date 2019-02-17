using Framework.Web.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Represents the framework base controller class.
    /// </summary>
    [FWExceptionFilter]
    public class FWBaseController : Controller
    {
    }
}
