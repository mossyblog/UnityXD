using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{/// <summary>
 /// Layout that arranges widgets in a row vertically.
 /// </summary>
    internal class XDVerticalLayout : XDLayout
    {
        public XDVerticalLayout(IXDLayout parent) : base(parent)
        {
        }

        public override void Render()
        {
            if (!enabled)
            {
                return;
            }

            if (style == null)
            {
                style = new GUIStyle();
                style.padding = new RectOffset(4, 4, 4, 4);
                style.margin = new RectOffset(4, 4, 4, 8);
            }
            GUILayout.BeginVertical(style);
            base.Render();
            GUILayout.EndVertical();
        }
    }
}
