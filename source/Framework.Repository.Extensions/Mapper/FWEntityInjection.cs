using Framework.Collections;
using Framework.Model;
using Framework.Model.Mapper.Implementation.ValueInjecter;
using System;
using System.Collections;
using System.Linq;

namespace Framework.Repository.Mapper
{
    class FWEntityInjection : FWInjection
    {
        private static Type _entity = typeof(FWEntity);

        protected override object InjectFrom(Type targetType, object source)
        {
            return FWMapperHelper.Transform<FWEntityInjection>(targetType, source);
        }

        protected override void OnCollectionMapped(object source, object target)
        {
            if (source is IFWList list)
            {
                var targetList = target as IList;
                var targetListGenericType = targetList.GetType().GetGenericArguments().First();
                if (_entity.IsAssignableFrom(targetListGenericType))
                {
                    foreach (var removed in list.RemovedItems)
                    {
                        var removedEntity = FWMapperHelper.Transform(targetListGenericType, removed);
                        (removedEntity as FWEntity).Remove();
                        targetList.Add(removedEntity);
                    }
                }
            }
        }
    }
}
