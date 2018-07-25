using Framework.Core;
using Framework.Web.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Binders
{
    /// <summary>
    /// Model binder for FWFormFile class.
    /// </summary>
    public class FWFormFileBinder : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.</param>
        /// <returns>A System.Threading.Tasks.Task which will complete when the model binding process completes.</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            // Check the value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                if (bindingContext.ModelType.IsGenericType)
                {
                    var result = new List<FWFormFile>();
                    foreach (var value in valueProviderResult.Values)
                    {
                        result.Add(BindFile(bindingContext, value).Result);
                    }
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
                else
                {
                    var result = BindFile(bindingContext, valueProviderResult.FirstValue).Result;
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
            }

            return Task.CompletedTask;
        }

        private async Task<FWFormFile> BindFile(ModelBindingContext bindingContext, string value)
        {
            var fileInfo = FWJsonHelper.Deserialize<FileData>(value);

            // Attempts to find the file at the tmp folder.
            var filePath = Path.Combine(FWApp.WebRootPath, "tmp", fileInfo.FileId);

            if (File.Exists(filePath))
            {
                var f = File.OpenRead(filePath);

                byte[] content = new byte[f.Length];
                await f.ReadAsync(content, 0, content.Length);

                return new FWFormFile()
                {
                    Content = content,
                    Length = f.Length,
                    FileName = fileInfo.FileName,
                    ContentType = fileInfo.ContentType
                };
            }

            return null;
        }

        private class FileData
        {
            public string FileId { get; set; }

            public string FileName { get; set; }

            public string ContentType { get; set; }
        }
    }
}
