using Framework.Web.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Elements
{
    class FWBindConfiguration
    {
        #region Constants
        public const string CHECKED = "checked";
        public const string CURRENCY = "currency";
        public const string DATEPICKER = "datepicker";            
        public const string SELECTED_OPTIONS = "selectedOptions";
        public const string TEXT = "text";
        public const string VALUE = "value";
        #endregion

        #region Operator overrides
        public static bool operator true(FWBindConfiguration val) => val._hasBind;
        public static bool operator false(FWBindConfiguration val) => !val._hasBind;
        public static bool operator !(FWBindConfiguration val) => !val._hasBind;
        #endregion

        /// <summary>
        /// Gets or sets the value binding of the control.
        /// </summary>
        public string BindName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the binding prefix.
        /// </summary>
        public string BindPrefix
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the custom expression defined by the user. Overrides all other configuration.
        /// </summary>
        public string CustomExpression
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Creates a string with all configurated bindings.
        /// </summary>
        /// <returns>The bindings string.</returns>
        public string CreateBind()
        {
            if (CustomExpression != null)
                return CustomExpression;

            StringBuilder sb = new StringBuilder();
            foreach (var item in _expression)
            {
                if (sb.Length > 0)
                {
                    sb.Append(",");
                }                
                sb.Append(item.Name);
                sb.Append(":");
                if (item.UsePrefix)
                {
                    sb.Append(BindPrefix);
                }
                sb.Append(item.Value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Adds the main binding value.
        /// </summary>
        /// <param name="bind">The binding name.</param>
        public void AddMainBind(string bind)
        {
            _expression.Add(new FWBindItem(bind, BindName, true));
        }

        /// <summary>
        /// Adds a custom binding.
        /// </summary>
        /// <param name="bind">The binding name.</param>
        /// <param name="value">The binding value.</param>
        /// <param name="usePrefix">Should the binding use prefix or not.</param>
        public void Add(string bind, string value, bool usePrefix = false)
        {
            _expression.Add(new FWBindItem(bind, value, usePrefix));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWBindConfiguration"/>
        /// </summary>
        /// <param name="bindName">The value binding name.</param>
        /// <param name="customExpression">The binding custom expression.</param>
        public FWBindConfiguration(string bindName, string customExpression)
        {
            _hasBind = true;
            CustomExpression = customExpression;
            BindName = bindName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWBindConfiguration"/>
        /// </summary>
        public FWBindConfiguration()
        { }
        
        private List<FWBindItem> _expression = new List<FWBindItem>();
        private bool _hasBind;
    }

    struct FWBindItem
    {
        public string Name
        {
            get;
            private set;
        }

        public string Value
        {
            get;
            private set;
        }

        public bool UsePrefix
        {
            get;
            set;
        }

        public FWBindItem(string name, string value, bool usePrefix)
        {
            Name = name;
            Value = value;
            UsePrefix = usePrefix;
        }
    }
}
