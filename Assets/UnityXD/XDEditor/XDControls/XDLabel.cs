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
    }

    /// <summary>
    ///     Widget for displaying read-only text.
    /// </summary>
    internal class XDLabel : XDWidget, IXDLabel
    {
        internal XDLabel(IXDLayout parent) : base(parent)
        {
        }


        public override void Render()
        {
            if (style == null)
            {
                style = new GUIStyle(EditorStyles.label);
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

            GUILayout.Label(content, style, layoutOptions.ToArray());
        }


    }
}