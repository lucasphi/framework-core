using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Represens an exception thrown when an invalid argument is passed to a parameter.
    /// </summary>
    public class FWArgumentException : FWException
    {
        /// <summary>
        /// Initializes a new instance of Framework.Core.FWArgumentException.
        /// </summary>
        /// <param name="argument"></param>
        public FWArgumentException(string argument)
            : base(string.Format(Resources.Resources.FWArgumentException, argument))
        { }
    }
}
