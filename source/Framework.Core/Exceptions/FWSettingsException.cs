using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core
{
    /// <summary>
    /// Represents an exception thrown when a resource is not found in the appsettings file.
    /// </summary>
    public class FWSettingsException : FWException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FWSettingsException"/>.
        /// </summary>
        /// <param name="section">The name of the section not found.</param>
        public FWSettingsException(string section) 
            : base(string.Format(Resources.Resources.FWSettingsException, section))
        { }
    }
}
