using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    /// Boolean check box widget.
    /// </summary>
    public interface IXDCheckbox : IXDWidget
    {
        /// <summary>
        /// Whether or not the box is checked.
        /// </summary>
        IPropertyBinding<bool, IXDCheckbox> Checked { get; }

        /// <summary>
        /// Text to display to the left of the check box.
        /// </summary>
        IPropertyBinding<string, IXDCheckbox> Label { get; }

        IXDCheckbox Selected(Expression<Func<bool>> expr);

        IXDCheckbox Text(string text);
    }

    /// <summary>
    /// Boolean check box widget.
    /// </summary>
    internal class XDCheckbox : XDWidget, IXDCheckbox
    {
        private bool boxChecked = false;
        private string label = String.Empty;

        private PropertyBinding<bool, IXDCheckbox> boxCheckedProperty;
        private PropertyBinding<string, IXDCheckbox> labelProperty;

        public IPropertyBinding<bool, IXDCheckbox> Checked { get { return boxCheckedProperty; } }
        public IPropertyBinding<string, IXDCheckbox> Label { get { return labelProperty; } }

        internal XDCheckbox(IXDLayout parent) : base(parent)
        {
            boxCheckedProperty = new PropertyBinding<bool, IXDCheckbox>(
                this,
                value => this.boxChecked = value
            );

            labelProperty = new PropertyBinding<string, IXDCheckbox>(
                this,
                value => this.label = value
            );
        }

        public IXDCheckbox Selected(Expression<Func<bool>> expr)
        {
            return boxCheckedProperty.BindTo(expr);
        }

        public IXDCheckbox Text(string text)
        {
            return labelProperty.Value(text);
        }

        public override void Render()
        {
            if (boxChecked != (boxChecked = EditorGUILayout.Toggle(label, boxChecked)))
            {
                boxCheckedProperty.UpdateView(boxChecked);
            }
        }

     
    }
}
