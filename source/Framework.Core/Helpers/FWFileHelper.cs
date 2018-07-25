using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Helper class to work with files.
    /// </summary>
    public static class FWFileHelper
    {
        /// <summary>
        /// Opens a file and get its content in a base 64 format.
        /// </summary>
        /// <param name="fullpath">The path to the file.</param>
        /// <returns>The base 64 formatted file.</returns>
        public static string GetBase64(string fullpath)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fullpath));
        }

        /// <summary>
        /// Creates a representation of an image from a file.
        /// </summary>
        /// <param name="imagepath">The path to the image.</param>
        /// <returns>The base 64 formatted image.</returns>
        public static string GetBase64Image(string imagepath)
        {
            return string.Format(@"data:image/png;base64,{0}", GetBase64(imagepath));
        }

        /// <summary>
        /// Creates a representation of an image.
        /// </summary>
        /// <param name="bytes">The image byte array.</param>
        /// <returns>The base 64 formatted image.</returns>
        public static string GetBase64Image(byte[] bytes)
        {
            return string.Format(@"data:image/png;base64,{0}", Convert.ToBase64String(bytes));
        }
    }
}
