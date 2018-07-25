using Framework.Model;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Portlet control.
    /// </summary>
    public class FWPortletControl : FWControlBlock
    {
        /// <summary>
        /// Gets or sets if the portlet allows fullscreen mode.
        /// </summary>
        public bool AllowFullscreen { get; set; }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns></returns>
        protected override IFWHtmlElement CreateControl()
        {
            var portlet = new FWDivElement();
            portlet.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                portlet.AddCssClass(CustomCss);

            portlet.Id = Id;
            portlet.DataType = _datatype ?? "fw-portlet";
            portlet.AddCssClass("m-portlet m-portlet--mobile");

            if (_color.HasValue)
                portlet.AddCssClass(_color.Value.GetDescription());

            var header = new FWDivElement
            {
                DataType = "fw-header"
            };
            if (_header != null)
            {
                CreatePortletHeader(header);
            }
            portlet.Add(header);

            var body = portlet.Add(new FWDivElement());
            body.AddCssClass("m-portlet__body");
            body.DataType = "fw-portlet-body";
            body.Add(ChildBody);

            if (_footer != null)
            {
                FWDivElement footer = portlet.Add(new FWDivElement());
                footer.AddCssClass("m-portlet__foot");
                footer.Add(_footer);
            }

            return portlet;
        }

        private void CreatePortletHeader(FWDivElement header)
        {            
            header.AddCssClass("m-portlet__head");

            var caption = new FWDivElement();
            caption.AddCssClass("m-portlet__head-caption");

            var title = new FWDivElement();
            title.AddCssClass("m-portlet__head-title");

            if (!string.IsNullOrWhiteSpace(_icon))
            {
                var iconSpan = new FWSpanElement();
                iconSpan.AddCssClass("m-portlet__head-icon");

                FWTagBuilder i = new FWTagBuilder("i");
                i.AddCssClass(_icon);
                iconSpan.Add(i);

                caption.Add(iconSpan);
            }

            var headerTitle = new FWTagBuilder("h3");
            {
                if (_required)
                {
                    headerTitle.Add("<span aria-required=\"true\" class=\"required\">*</span>");
                }

                headerTitle.AddCssClass("m-portlet__head-text");
                headerTitle.Add(_header);

                title.Add(headerTitle);
            }

            caption.Add(title);
            header.Add(caption);

            var actions = HeaderActions();
            header.Add(actions);
        }

        private FWDivElement HeaderActions()
        {
            if (AllowFullscreen)
            {
                var fullscreenOpts = new FWListItemElement();
                fullscreenOpts.AddCssClass("m-portlet__nav-item");
                var anchor = new FWAnchorElement("#");
                anchor.Attributes.Add("data-portlet-tool", "fullscreen");
                anchor.Attributes.Add("data-expand", ViewResources.Portlet_Fullscreen);
                anchor.Attributes.Add("data-compress", ViewResources.Portlet_FullscreenUndo);
                anchor.AddCssClass("m-portlet__nav-link m-portlet__nav-link--icon");
                anchor.Add("<i class=\"fa fa-expand\" aria-hidden=\"true\"></i>");
                anchor.Add("<i class=\"fa fa-compress\" aria-hidden=\"true\"></i>");
                fullscreenOpts.Add(anchor);

                var acts = GetPortletActions();
                acts.Add(fullscreenOpts);
            }

            var actions = new FWDivElement();
            actions.AddCssClass("m-portlet__head-tools");

            if (_actions != null)
                actions.Add(_actions.ToString());

            return actions;
        }

        /// <summary>
        /// Sets an image for the portlet header.
        /// </summary>
        /// <param name="icon">The image name.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWPortletControl Icon(string icon)
        {
            _icon = icon;
            return this;
        }

        /// <summary>
        /// Adds a portlet action.
        /// </summary>
        /// <param name="actionBody">The string containing the widget tags.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWPortletControl AddAction(string actionBody)
        {
            var actionItem = new FWListItemElement();
            actionItem.AddCssClass("m-portlet__nav-item");
            actionItem.Add(actionBody);

            var actions = GetPortletActions();
            actions.Add(actionItem);
            return this;
        }

        /// <summary>
        /// Adds a portlet action.
        /// </summary>
        /// <param name="button">The button to be added.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWPortletControl AddAction(FWButtonControl button)
        {
            AddAction(button.ToString());
            return this;
        }

        /// <summary>
        /// Sets the portlet footer.
        /// </summary>
        /// <param name="footer">The string containing the footer tags.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWPortletControl Footer(string footer)
        {
            _footer = footer;
            return this;
        }

        /// <summary>
        /// Sets the portlet color.
        /// </summary>
        /// <param name="color">The portlet color.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWPortletControl Color(FWStateColors color)
        {
            _color = color;
            return this;
        }

        /// <summary>
        /// Sets the internal control as required.
        /// </summary>
        /// <param name="required">Informs if the internal control is required.</param>
        /// <returns>The fluent configurator object.</returns>
        internal FWPortletControl Required(bool required = true)
        {
            _required = required;
            return this;
        }

        /// <summary>
        /// Changes the portlet datatype.
        /// </summary>
        /// <param name="dataType">The new datatype.</param>
        /// <returns>The fluent configurator object.</returns>
        internal FWPortletControl DataType(string dataType)
        {
            _datatype = dataType;
            return this;
        }

        /// <summary>
        /// Gets the portlet action list element.
        /// </summary>
        private FWListElement GetPortletActions()
        {
            if (_actions == null)
            {
                _actions = new FWListElement();
                _actions.AddCssClass("m-portlet__nav");
            }
            return _actions;
        }

        /// <summary>
        /// Creates a new Portlet.
        /// </summary>
        /// <param name="id">The control id.</param>
        /// <param name="header">The header text.</param>
        public FWPortletControl(string id, string header)
            : base(id)
        {
            _header = header;
        }

        private FWStateColors? _color;
        private string _datatype;

        private FWListElement _actions;
        private string _footer;
        private string _header;
        private string _icon;

        private bool _required;
    }
}
