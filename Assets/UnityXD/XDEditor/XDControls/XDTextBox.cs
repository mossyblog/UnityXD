using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    /// Widget for entering text.
    /// </summary>
    public interface IXDTextBox : IXDWidget
    {
        /// <summary>
        /// Editable text.
        /// </summary>
        IPropertyBinding<string, IXDTextBox> Text { get; }

        IXDTextBox Label(string label, int labelW);
        IXDTextBox FormValue(Expression<Func<string>> expr);
        IXDTextBox FormValue(Expression<Func<int>> expr);
        IXDTextBox FormValue(Expression<Func<double>> expr);
    }

    /// <summary>
    /// Widget for entering text.
    /// </summary>
    internal class XDTextBox : XDWidget, IXDTextBox
    {
        private string text = string.Empty;
        private double textD;
        private int textI;
        private string _label = string.Empty;

        private PropertyBinding<string, IXDTextBox> textProperty;
        private PropertyBinding<int, IXDTextBox> intProperty;
        private PropertyBinding<double, IXDTextBox> doubleProperty;

        public IPropertyBinding<string, IXDTextBox> Text { get { return textProperty; } }
        public IPropertyBinding<int, IXDTextBox> TextInt { get { return intProperty; } }
        public IPropertyBinding<double, IXDTextBox> TextDouble { get { return doubleProperty; } }

        private bool isText;
        private bool isInt;
        private bool isDouble;
        private int _labelW;

        internal XDTextBox(IXDLayout parent) : base(parent)
        {
            textProperty = new PropertyBinding<string, IXDTextBox>(this,value => text = value ?? string.Empty);
            intProperty = new PropertyBinding<int, IXDTextBox>(this, value => textI = value);
            doubleProperty = new PropertyBinding<double, IXDTextBox>(this, value => textD = value);

        }

        public IXDTextBox FormValue(Expression<Func<string>> expr)
        {
            isText = true;
            return textProperty.BindTo(expr);
        }

        public IXDTextBox FormValue(Expression<Func<double>> expr)
        {
            isDouble = true;
            return doubleProperty.BindTo(expr);
        }

        public IXDTextBox FormValue(Expression<Func<int>> expr)
        {
            isInt = true;
            return intProperty.BindTo(expr);
        }

        public IXDTextBox Label(string label, int labelW = -1)
        {
            _label = label;
            _labelW = labelW;
            return this;
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

 

            var content = new GUIContent(_label);
            using (new EditorGUILayout.HorizontalScope())
            {

                if(_labelW >0)
                    GUILayout.Label(content, GUILayout.Width(_labelW));
                else
                    GUILayout.Label(content);


                GUILayout.Space(8);
                if (isText)
                {
                    string newText = height >= 0 // Use TextField if height isn't specified, otherwise use TextArea
                        ? GUILayout.TextArea(text, layoutOptions.ToArray())
                        : GUILayout.TextField(text, layoutOptions.ToArray());

                    if (newText != text)
                    {
                        text = newText;
                        textProperty.UpdateView(newText);
                    }
                }

                if (isDouble)
                {

                    textD = EditorGUILayout.DoubleField(textD);
                    doubleProperty.UpdateView(textD);
                }

                if (isInt)
                {
                    textI = EditorGUILayout.IntField(textI, layoutOptions.ToArray());
                    intProperty.UpdateView(textI);
                }
            }
        }

        internal override void BindViewModel(object viewModel)
        {
            doubleProperty.BindViewModel(viewModel);
            intProperty.BindViewModel(viewModel);
            textProperty.BindViewModel(viewModel);
  
        }
    }
}
