using Framework.Collections;
using Framework.Core;
using Framework.Web.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace Framework.Web.Mvc.Binders
{
    /// <summary>
    /// Framework implementation of the Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinderProvider interface.
    /// </summary>
    public class FWModelBinderProvider : IModelBinderProvider
    {
        private static Type _fwList = typeof(FWList<>);
        private static Type _detailListBinder = typeof(FWListBinder<>);
        private static Type _formFile = typeof(FWFormFile);

        /// <summary>
        /// Looks for an implementation of the Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder within the framework.
        /// </summary>
        /// <param name="context">The Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext.</param>
        /// <returns>An Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder.</returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Metadata.UnderlyingOrModelType == FWKnownTypes.Decimal)
            {
                return new FWDecimalModelBinder();
            }

            if (context.Metadata.UnderlyingOrModelType == _formFile
                    || context.Metadata.UnderlyingOrModelType.IsGenericType && context.Metadata.UnderlyingOrModelType.GetGenericArguments().First().UnderlyingSystemType == _formFile)
            {
                return new FWFormFileBinder();
            }

            if (context.Metadata.ModelType.IsConstructedGenericType
                    && context.Metadata.ModelType.GetGenericTypeDefinition() == _fwList)
            {
                var genericType = _detailListBinder.MakeGenericType(context.Metadata.ModelType.GenericTypeArguments);
                return Activator.CreateInstance(genericType, context.CreateBinder(context.Metadata.ElementMetadata)) as IModelBinder;
            }

            return null;
        }
    }
}
