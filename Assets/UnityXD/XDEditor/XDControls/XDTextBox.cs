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
        IXDTextBox Field(Action<string> action, string fieldValue);
        IXDTextBox Field (Action<int> action, int fieldValue);
        IXDTextBox Field(Action<double> action, double fieldValue);

        IXDTextBox Label(GUIStyle style);
        IXDTextBox Label(string text);
        IXDTextBox Label(String text, GUIStyle style);
        IXDTextBox Label(String text, int labelW, GUIStyle style);
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

        private bool isText;
        private int _labelW;
        private Action<string> actionString;
        private Action<int> actionInt;
        private Action<double> actionDouble;

        private GUIStyle labelStyle;

        internal XDTextBox(IXDLayout parent) : base(parent)
        {

        }

        public IXDTextBox Field(Action<string> callback, string fieldValue)
        {
            text = fieldValue;
            actionString = callback;
            return this;
        }

        public IXDTextBox Field(Action<int> callback, int fieldValue)
        {
            textI = fieldValue;
            actionInt = callback;
            return this;
        }

        public IXDTextBox Field(Action<double> callback, double fieldValue)
        {
            textD = fieldValue;
            actionDouble = callback;
            return this;
        }

        public IXDTextBox Label(GUIStyle style)
        {
            labelStyle = style;
            return this;
        }

        public IXDTextBox Label(string text)
        {
            content.text = text;
            return this;
        }


        public IXDTextBox Label(string text, GUIStyle style)
        {
            labelStyle = style;
            content.text = text;
            return this;
        }
        public IXDTextBox Label(string text, int labelW, GUIStyle style)
        {
            labelStyle = style;
            content.text = text;
            _labelW = labelW;
            return this;
        }

        public override void Render()
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

            
 

            using (new EditorGUILayout.HorizontalScope())
            {

                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(GUI.skin.label);
                }

                if(_labelW >0)
                    GUILayout.Label(content, labelStyle, GUILayout.Width(_labelW));
                else
                    GUILayout.Label(content, labelStyle);


                if (!OverrideStyle)
                {
                    style = new GUIStyle(GUI.skin.textField);
                }

                if (actionString != null)
                {
                    var newText = height >= 0 ? GUILayout.TextArea(text, layoutOptions.ToArray()) : GUILayout.TextField(text, layoutOptions.ToArray());
                    if (newText != text)
                    {
                        actionString.DynamicInvoke(newText);
                    }
                }

                if (actionInt != null)
                {
                    var newtext = EditorGUILayout.IntField(textI, style, layoutOptions.ToArray());
                    if (newtext != textI)
                    {
                        actionInt.DynamicInvoke(newtext);
                    }
                }

                if (actionDouble != null)
                {
                    var newtext = EditorGUILayout.DoubleField(textI, style, layoutOptions.ToArray());
                    if (newtext != textI)
                    {
                        actionDouble.DynamicInvoke(newtext);
                    }
                }

            }
        }

    }
}
