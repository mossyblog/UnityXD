using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    ///     Widget for displaying read-only text.
    /// </summary>
    public interface IXDLabel : IXDWidget
    {
        /// <summary>
        ///     Whether or not the label should be displayed in bold (default is false).
        /// </summary>
        IPropertyBinding<bool, IXDLabel> Bold { get; }

        /// <summary>
        ///     XDLabel text.
        /// </summary>
        IXDLabel Text(string value);

        /// <summary>
        ///     XDLabel text.
        /// </summary>
        IXDLabel Text(Expression<Func<string>> propertyExpression);
    }

    /// <summary>
    ///     Widget for displaying read-only text.
    /// </summary>
    internal class XDLabel : XDWidget, IXDLabel
    {
        private readonly PropertyBinding<bool, IXDLabel> boldProperty;
        private readonly PropertyBinding<string, IXDLabel> textProperty;

        private bool _bold;
        private string _text = string.Empty;

        internal XDLabel(IXDLayout parent) : base(parent)
        {
            textProperty = new PropertyBinding<string, IXDLabel>(this, value => _text = value);
            boldProperty = new PropertyBinding<bool, IXDLabel>(this, value => _bold = value);
        }

        public IXDLabel Text(string value)
        {
            return textProperty.Value(value);
        }

        public IXDLabel Text(Expression<Func<string>> propertyExpression)
        {
            return textProperty.BindTo(propertyExpression);
        }

        public IPropertyBinding<bool, IXDLabel> Bold
        {
            get { return boldProperty; }
        }

        public override void OnGUI()
        {
            var guiContent = new GUIContent(_text, tooltip);
            if (_style == null)
            {
                _style = _bold ? new GUIStyle(EditorStyles.boldLabel) : new GUIStyle(EditorStyles.label);
            }
            else
            {
                if (_bold)
                {
                    _style = new GUIStyle(_style) {fontStyle = FontStyle.Bold};
                }
            }

            var layoutOptions = new List<GUILayoutOption>();

            if (width >= 0)
            {
                layoutOptions.Add(GUILayout.Width(width));
            }

            if (height >= 0)
            {
                layoutOptions.Add(GUILayout.Height(height));
            }

            GUILayout.Label(guiContent, _style, layoutOptions.ToArray());
        }

        internal override void BindViewModel(object viewModel)
        {
            base.BindViewModel(viewModel);
            textProperty.BindViewModel(viewModel);
            boldProperty.BindViewModel(viewModel);
        }
    }
}