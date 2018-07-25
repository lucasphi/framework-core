using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Model.Mapper
{
    /// <summary>
    /// Defines the interface for custom mappers.
    /// </summary>
    public interface IFWCustomMapper
    {
        /// <summary>
        /// Maps a source object to a target.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="target">The target object.</param>
        /// <param name="targetInfo">The target member info.</param>
        void SetValue(object source, object target, MemberInfo targetInfo);
    }
}
