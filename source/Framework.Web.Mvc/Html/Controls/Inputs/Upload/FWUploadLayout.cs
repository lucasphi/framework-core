using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Enumerates the upload layouts.
    /// </summary>
    public enum FWUploadLayout
    {
        /// <summary>
        /// The default framework layout that mimics an input.
        /// </summary>
        Input,

        /// <summary>
        /// The dropzone upload layout.
        /// </summary>
        Dropzone,

        /// <summary>
        /// A custom layout.
        /// </summary>
        Custom
    }
}
