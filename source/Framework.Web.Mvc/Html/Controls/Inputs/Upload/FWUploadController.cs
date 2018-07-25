using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Controls.Inputs.Upload
{
    /// <summary>
    /// Framework upload controller.
    /// </summary>
    public class FWUploadController : FWBaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="FWUploadController" />.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment service.</param>
        public FWUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Action called to upload a file.
        /// </summary>
        /// <param name="file">The file uploaded.</param>
        /// <returns>Return a json with the uploaded file id to be binded later.</returns>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var tmpFolder = Path.Combine(_hostingEnvironment.WebRootPath, "tmp");

            if (!Directory.Exists(tmpFolder))
                Directory.CreateDirectory(tmpFolder);

            ClearFolder(tmpFolder);

            var fileId = Guid.NewGuid() + Path.GetExtension(file.FileName);
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(tmpFolder, fileId), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return Json(new { fileId, file.FileName, file.ContentType });
        }

        /// <summary>
        /// Clears the old uploaded files.
        /// </summary>
        /// <param name="folder">The folder path.</param>
        private void ClearFolder(string folder)
        {            
            var files = Directory.GetFiles(folder);
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                if (fi.CreationTime.AddDays(1) <= DateTime.Now)
                {
                    fi.Delete();
                }
            }
        }
    }
}
