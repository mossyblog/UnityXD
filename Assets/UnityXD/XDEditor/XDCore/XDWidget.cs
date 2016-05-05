using Assets.UnityXD.Core;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDCore
{
    /// <summary>
    /// Abstract class that all other widgets must implement.
    /// </summary>
    internal abstract class XDWidget : IXDWidget
    {
        private readonly PropertyBinding<string, IXDWidget> tooltipProperty;
        private readonly PropertyBinding<int, IXDWidget> widthProperty;
        private readonly PropertyBinding<int, IXDWidget> heightProperty;
        
        protected string tooltip = string.Empty;
        protected int height = -1;
        protected int width = -1;
        protected GUIStyle _style;

        /// <summary>
        /// Needed in order to get back to the parent via the End() method.
        /// </summary>
        private IXDLayout parent;


        public IXDWidget Tooltip(string value)
        {
            return tooltipProperty.Value(value);
        }

        public IXDWidget Width(int value)
        {
            return widthProperty.Value(value);
        }
        public IXDWidget Height(int value)
        {
            return heightProperty.Value(value);
        }

        public IXDWidget Style(GUIStyle style)
        {
            _style = style;
            return this;
        }
        
        /// <summary>
        /// Creates the widget and sets its parent.
        /// </summary>
        protected XDWidget(IXDLayout parent)
        {
            this.parent = parent;
            tooltipProperty = new PropertyBinding<string, IXDWidget>(this, value => tooltip = value);
            widthProperty = new PropertyBinding<int, IXDWidget>(this, value => width = value);
            heightProperty = new PropertyBinding<int, IXDWidget>(this, value => height = value);
        }

        /// <summary>
        /// Updates this widget and all children (if it is a layout)
        /// </summary>
        public abstract void OnGUI();

        /// <summary>
        /// Binds the properties and events in this widget to corrosponding ones in the supplied view model.
        /// </summary>
        internal virtual void BindViewModel(object viewModel)
        {
            tooltipProperty.BindViewModel(viewModel);
            widthProperty.BindViewModel(viewModel);
            heightProperty.BindViewModel(viewModel);
        }

        /// <summary>
        /// Fluent API for getting the layout containing this widget.
        /// </summary>
        public IXDLayout End()
        {
            return parent;
        }


        public  Texture2D CreateColoredTexture(Color col)
        {
            var width = 10;
            var height = 10;
            var pix = new Color[width * height];

            for (var i = 0; i < pix.Length; i++)
                pix[i] = col;

            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
    }

}
