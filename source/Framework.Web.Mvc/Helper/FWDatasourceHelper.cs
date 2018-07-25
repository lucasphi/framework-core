using Framework.Web.Mvc.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Web.Mvc.Helper
{
    static class FWDatasourceHelper
    {
        public static IEnumerable<IFWDatasourceItem> GetDatasourceFromModel(string datasourceName, object model)
        {
            if (model == null)
                return null;

            var modelType = model.GetType();
            var property = modelType.GetProperty(datasourceName);
            if (property != null)
            {
                var datasource = property.GetValue(model) as IEnumerable;
                if (datasource == null)
                    return new List<IFWDatasourceItem>();

                return datasource.Cast<IFWDatasourceItem>();
            }
            return null;
        }
    }
}
