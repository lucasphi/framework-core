using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Features
{
    /// <summary>
    /// Represents a file sent with the HttpRequest.
    /// </summary>
    public class FWFormFile
    {
        /// <summary>
        /// Gets or sets the raw Content-Type of the uploaded file.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the file length in bytes.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Gets or sets the file name from the Content-Disposition header.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file content.
        /// </summary>
        public byte[] Content { get; set; }
    }
}
