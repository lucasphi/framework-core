using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Framework.Core;
using Framework.Model.Mapper;

namespace Framework.Model.Tests.Helpers
{
    public class CustomMapper : IFWInjection
    {
        public object Map(object source, object target)
        {
            return Activator.CreateInstance(target.GetType());
        }
    }

    public class CustomProperty : IFWCustomMapper
    {
        public void SetValue(object source, object target, MemberInfo targetInfo)
        {
            targetInfo.SetValue(target, 2);
        }
    }
}
