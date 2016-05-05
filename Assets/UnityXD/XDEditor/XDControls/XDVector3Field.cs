using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    /// Widget for entering vectors with X, Y and Z coordinates.
    /// </summary>
    public interface IXDVector3Field : IXDWidget
    {
        /// <summary>
        /// Label shown to the left of the widget.
        /// </summary>
        IPropertyBinding<string, IXDVector3Field> Label { get; }


        /// <summary>
        /// Vector entered in widget.
        /// </summary>
        IPropertyBinding<Vector3, IXDVector3Field> Vector { get; }
    }

    /// <summary>
    /// Widget for entering vectors with X, Y and Z coordinates.
    /// </summary>
    internal class XDVector3Field : XDWidget, IXDVector3Field
    {
        private Vector3 vector;
        private string label;
  

        private PropertyBinding<Vector3, IXDVector3Field> vectorProperty;
        private PropertyBinding<string, IXDVector3Field> labelProperty;


        public IPropertyBinding<Vector3, IXDVector3Field> Vector { get { return vectorProperty; } }
        public IPropertyBinding<string, IXDVector3Field> Label { get { return labelProperty; } }

        internal XDVector3Field(IXDLayout parent) : base(parent)
        {
            vectorProperty = new PropertyBinding<Vector3, IXDVector3Field>(
                this,
                value => this.vector = value
            );

            labelProperty = new PropertyBinding<string, IXDVector3Field>(
                this,
                value => this.label = value
            );


        }

        public override void OnGUI()
        {
            var newVector = EditorGUILayout.Vector3Field(new GUIContent(label, tooltip), vector);
            if (newVector != vector)
            {
                vector = newVector;
                vectorProperty.UpdateView(vector);
            }
        }

        internal override void BindViewModel(object viewModel)
        {
            vectorProperty.BindViewModel(viewModel);
            labelProperty.BindViewModel(viewModel);
        }
    }
}
