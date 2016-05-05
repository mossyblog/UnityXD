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

        public override void OnGUI()
        {
            if (!enabled)
            {
                return;
            }
            var pStyle = new GUIStyle();
            pStyle.padding = new RectOffset(4, 4, 4, 8);
            pStyle.margin = new RectOffset(0,0,8, 8);
            GUILayout.BeginVertical(pStyle);
            base.OnGUI();
            GUILayout.EndVertical();
        }
    }
}
