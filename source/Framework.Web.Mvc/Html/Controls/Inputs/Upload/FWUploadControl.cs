using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Html.Controls;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Represents an Upload control.
    /// </summary>
    public class FWUploadControl : FWInputControl
    {
        /// <summary>
        /// Configures the control based on the model metadata.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        protected override void ReadMetadata(ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(nameof(FWUploadAttribute)))
            {
                var config = metadata.AdditionalValues[nameof(FWUploadAttribute)] as FWUploadAttribute;
                _maxFiles = config.MaxFiles;
                _maxFileSize = config.MaxFileSize;
                _allowedMimes = config.AllowedMimes;
            }
        }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement();
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.Id = Id;
            element.DataType = "fw-fileupload";

            element.Attributes.Add("data-maxfiles", _maxFiles.ToString());
            element.Attributes.Add("data-maxfilesdsc", ViewResources.Upload_File_Limit);
            element.Attributes.Add("data-maxfilesize", _maxFileSize.ToString());
            element.Attributes.Add("data-maxfilesizedsc", ViewResources.Upload_File_Size_Limit);
            if (_allowedMimes != null)
            {
                element.Attributes.Add("data-allowedmimes", string.Join(",", _allowedMimes));
                element.Attributes.Add("data-allowedmimesdsc", ViewResources.Upload_File_Invalid);
            }

            if (DisplayLabel)
            {
                FWLabelControl label = new FWLabelControl(Name, DisplayName, IsRequired, Tooltip);
                label.AddCssClass("control-label");
                element.Add(label.ToString());
            }

            FWDivElement dropzone = CreateDropzone();
            switch(_layout)
            {
                case FWUploadLayout.Input:
                    AddInputLayout(dropzone);
                    break;
                case FWUploadLayout.Dropzone:                    
                    AddDropzoneLayout(dropzone);
                    break;
                default:
                    dropzone.Attributes.Add("data-theme", "custom");
                    dropzone.Add(LayoutBody);
                    break;
            }

            element.Add(dropzone.ToString());

            return element;
        }

        private FWDivElement CreateDropzone()
        {
            var dropzone = new FWDivElement();                        
            dropzone.Attributes.Add("data-type", "body");
            dropzone.Attributes.Add("data-name", Name);
            dropzone.Attributes.Add("action", RequestContext.Url.Action("Upload", "FWUpload", new { area = string.Empty }));            

            return dropzone;
        }

        private void AddInputLayout(FWDivElement body)
        {
            body.AddCssClass("upload");
            body.Attributes.Add("data-theme", "input");

            body.Add($"<div data-type=\"upload-template\" data-toggle=\"popover\" class=\"dz-preview\"><img data-dz-thumbnail style=\"display:none\" /><span data-dz-name></span><span data-dz-progress></span><span data-type=\"Remove\" class=\"m--font-danger\" title=\"{ViewResources.Btn_Upload_Remove}\"><i class=\"fas fa-ban\"></i></span></div>");

            body.Add("<div data-type=\"upload-preview\" class=\"upload-preview\"></div>");

            var button = new FWButtonControl($"{Id}_btn_upload")
            {
                Text = _maxFiles == 1 ? ViewResources.Btn_Upload : ViewResources.Btn_Upload_Multiple,
                DataType = "upload-clickable"
            };
            button.Icon("fas fa-upload");
            button.Size(FWElementSize.Small);
            body.Add(button.ToString());
        }

        private void AddDropzoneLayout(FWDivElement body)
        {
            body.AddCssClass("dropzone m-dropzone");
            body.Attributes.Add("data-theme", "dropzone");
            body.Attributes.Add("data-remove", ViewResources.Btn_Upload_Remove);

            body.Add($"<div class=\"dz-message\">{GetModelResource("ChooseFiles")}</div>");
        }

        /// <summary>
        /// Creates a new Textbox control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="layout">The upload content layout.</param>
        public FWUploadControl(FWRequestContext requestContext, object model, ModelMetadata metadata, FWUploadLayout layout)
            : base(requestContext, model, metadata)
        {
            _layout = layout;
        }

        internal string LayoutBody { get; set; }

        private readonly FWUploadLayout _layout;

        private int _maxFiles = 1;
        private string[] _allowedMimes;
        private int _maxFileSize = 5;
    }
}
