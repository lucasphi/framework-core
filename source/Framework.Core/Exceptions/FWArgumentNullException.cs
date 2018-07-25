using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Represents an exception thrown when a null reference is passed as an argument.
    /// </summary>
    public class FWArgumentNullException : FWException
    {
        /// <summary>
        /// Initializes a new instance of FWArgumentNullException.
        /// </summary>
        /// <param name="param">Name of the parameter.</param>
        public FWArgumentNullException(string param)
            : base(string.Format(Resources.Resources.FWArgumentNullException, param))
        { }
    }
}
