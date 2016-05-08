using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{/// <summary>
 /// Inserts a space between other widgets.
 /// </summary>
public interface IXDFlexiSpace : IXDWidget
    {

    }

    /// <summary>
    /// Inserts a space between other widgets.
    /// </summary>
    internal class XDFlexiSpace : XDWidget, IXDFlexiSpace
    {
        internal XDFlexiSpace(IXDLayout parent) : base(parent)
        {

        }

        public override void Render()
        {
            GUILayout.FlexibleSpace();
        }


    }
}
