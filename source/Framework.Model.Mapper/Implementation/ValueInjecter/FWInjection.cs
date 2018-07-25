using Framework.Core;
using Omu.ValueInjecter.Injections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Model.Mapper.Implementation.ValueInjecter
{
    /// <summary>
    /// Customizations for the ValueInjecter mapper.
    /// </summary>
    public class FWInjection : ValueInjection, IFWInjection
    {
        #region Injection Handler
        /// <summary>
        /// Maps a source object to a target.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="target">The target object.</param>
        protected sealed override void Inject(object source, object target)
        {
            if (source == null)
                return;

            var targetType = target.GetType();            

            // Checks to see if its mapping a list
            if (FWReflectionHelper.IsCollection(targetType))
            {
                InjectCollection(source, target, targetType);
            }
            else
            {
                InjectMember(source, target, targetType);
            }
        }

        /// <summary>
        /// Maps a source object to a target type.
        /// </summary>
        /// <param name="targetType">The target type.</param>
        /// <param name="source">The source object.</param>
        /// <returns>The mapped object.</returns>
        protected virtual object InjectFrom(Type targetType, object source)
        {
            return FWMapperHelper.Transform<FWInjection>(targetType, source);
        }
        #endregion

        #region Property Injecter
        /// <summary>
        /// Maps a property.
        /// </summary>
        protected virtual void InjectMember(object source, object target, Type targetType)
        {
            var properties = FWReflectionHelper.GetPublicProperties(targetType, false);
            InjectMemberProperties(source, target, properties);

            InjectPrivateMembers(source, target, targetType);
        }

        private void InjectPrivateMembers(object source, object target, Type targetType)
        {
            var privateMembers = FWReflectionHelper.GetMembers(targetType, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(f => f.IsDefined(typeof(FWMapPrivateAttribute)));
            if (privateMembers.Any())
            {
                InjectMemberProperties(source, target, privateMembers);
            }
        }

        private void InjectMemberProperties(object source, object target, IEnumerable<MemberInfo> targetMembers)
        {
            var sourceType = source.GetType();

            foreach (var targetMember in targetMembers)
            {
                try
                {
                    // Checks if the property is not ignored.
                    if (!targetMember.IsDefined(typeof(FWMapperIgnoreAttribute)))
                    {
                        // Checks if the current member has a custom mapper defined.
                        var propertyMapper = targetMember.GetCustomAttribute<FWMapCustomAttribute>();
                        if (propertyMapper == null)
                        {
                            // Checks to see if the property is decorated with FWMapName attribute.
                            var nameAttributes = targetMember.GetCustomAttributes<FWMapNameAttribute>();

                            if (!nameAttributes.Any())
                            {
                                // If not, calls the default scope.
                                SetValue(source, target, sourceType.GetMember(targetMember.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).First(), targetMember);
                            }
                            else
                            {
                                // If the property is decorated, use the informed Name to map.
                                foreach (var attr in nameAttributes)
                                {
                                    var namedSourceMember = sourceType.GetMember(attr.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();
                                    if (namedSourceMember != null)
                                        SetValue(source, target, namedSourceMember, targetMember);
                                }
                            }
                        }
                        else
                        {
                            // Checks if the custom mapper has defined the proper interface.
                            if (Activator.CreateInstance(propertyMapper.Mapper, true) is IFWCustomMapper mapper)
                            {
                                mapper.SetValue(source, target, targetMember);
                            }
                        }
                    }
                }
                catch (Exception)
                { /* Prevents any mapper errors to interrupt the flow. */ }
            }
        }

        private void SetValue(object source, object target, MemberInfo sourceMember, MemberInfo targetMember)
        {
            var targetType = targetMember.GetMemberType();
            if (!FWReflectionHelper.IsPrimitive(targetType))
            {
                var complexObject = sourceMember.GetValue(source);
                if (complexObject != null)
                {
                    // Gets the target object property value
                    var targetMemberValue = targetMember.GetValue(target);

                    // Gets the property type or, if it is an interface, from the source object type
                    var type = (!targetType.IsInterface) ? 
                                    targetType : 
                                    complexObject.GetType();

                    if (targetMemberValue == null)                        
                        targetMemberValue = Activator.CreateInstance(type);

                    targetMemberValue = InjectFrom(type, complexObject);
                    targetMember.SetValue(target, targetMemberValue);
                }
            }
            else
            {
                var val = sourceMember.GetValue(source);
                // Checks to see if there are any maps for the primitive.
                val = Omu.ValueInjecter.Mapper.Instance.MapPrimitive(targetType, val);
                targetMember.SetValue(target, val);
            }
        }
        #endregion

        #region CollectionInjecter
        /// <summary>
        /// Maps a collection.
        /// </summary>
        protected virtual void InjectCollection(object source, object target, Type targetType)
        {
            if (source is IEnumerable sourceList)
            {
                if (_listType.IsAssignableFrom(targetType))
                {
                    var targetList = target as IList;
                    InjectCollectionItems(sourceList, targetList, targetType);
                    InjectPrivateMembers(source, target, targetType);

                    OnCollectionMapped(source, target);
                }
                else if (_dictionaryType.IsAssignableFrom(targetType))
                {
                    InjectDictionary(source as IDictionary, target as IDictionary, targetType);
                    InjectPrivateMembers(source, target, targetType);
                }
            }
        }

        private void InjectCollectionItems(IEnumerable source, IList target, Type targetType)
        {
            var tmpList = target as IList;
            var itemType = target.GetType().GetGenericArguments().First();

            // Maps the collection items.
            foreach (var item in source as IEnumerable)
            {
                var newItem = InjectFrom(itemType, item);
                tmpList.Add(newItem);
            }
        }

        private void InjectDictionary(IDictionary dictionary, IDictionary target, Type targetType)
        {
            foreach (DictionaryEntry item in dictionary)
            {
                // Gets the value type.
                var itemValueType = targetType.GetGenericArguments().ElementAt(1);

                // If the list is for primitive values, just add the value to the list
                if (FWReflectionHelper.IsPrimitive(itemValueType))
                {
                    target.Add(item.Key, item.Value);
                }
                else
                {
                    if (item.Value != null)
                    {
                        // If the list is for complex types, try to map each item.
                        var newItem = InjectFrom(itemValueType, item.Value);
                        target.Add(item.Key, newItem);
                    }
                    else
                    {
                        target.Add(item.Key, null);
                    }
                }
            }
        }
        #endregion

        #region Callbacks
        /// <summary>
        /// Executes an action after a collection has been mapped.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <param name="target">The target collection.</param>
        protected virtual void OnCollectionMapped(object source, object target)
        { }
        #endregion

        #region Fields
        private static Type _listType = typeof(IList);
        private static Type _dictionaryType = typeof(IDictionary);
        #endregion
    }
}
