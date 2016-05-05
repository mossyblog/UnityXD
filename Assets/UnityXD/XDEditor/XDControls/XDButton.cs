using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    /// Clickable push button widget.
    /// </summary>
    public interface IXDButton : IXDWidget
    {
        /// <summary>
        /// Text to be displayed on the button.
        /// </summary>
        IPropertyBinding<string, IXDButton> Text { get; }

        /// <summary>
        /// Event invoked when the button is clicked.
        /// </summary>
        IEventBinding<IXDButton> Click { get; }
    }

    /// <summary>
    /// Clickable push button widget.
    /// </summary>
    internal class XDButton : XDWidget, IXDButton
    {
        // Private members
        private string text = String.Empty;

        // Concrete property bindings
        private PropertyBinding<string, IXDButton> textProperty;
        private EventBinding<IXDButton> clickEvent;

        // Public interfaces for getting PropertyBindings
        public IPropertyBinding<string, IXDButton> Text { get { return textProperty; } }
        public IEventBinding<IXDButton> Click { get { return clickEvent; } }

        internal XDButton(IXDLayout parent) : base(parent)
        {
            textProperty = new PropertyBinding<string, IXDButton>(
                this,
                value => this.text = value
            );

            clickEvent = new EventBinding<IXDButton>(this);
        }

        public override void OnGUI()
        {
            var layoutOptions = new List<GUILayoutOption>();
            if (width >= 0)
            {
                layoutOptions.Add(GUILayout.Width(width));
            }
            if (height >= 0)
            {
                layoutOptions.Add(GUILayout.Height(height));
            }

            if (GUILayout.Button(new GUIContent(text, tooltip), layoutOptions.ToArray()))
            {
                clickEvent.Invoke();
            }
        }

        internal override void BindViewModel(object viewModel)
        {
            base.BindViewModel(viewModel);
            textProperty.BindViewModel(viewModel);
            clickEvent.BindViewModel(viewModel);
        }
    }
}
