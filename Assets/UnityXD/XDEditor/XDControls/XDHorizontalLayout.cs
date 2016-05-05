using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    internal class HorizontalLayout : XDLayout
    {
        public HorizontalLayout(IXDLayout parent) : base(parent)
        {
        }

        public override void OnGUI()
        {
            if (!enabled)
            {
                return;
            }
            var pStyle = new GUIStyle();
            pStyle.padding = new RectOffset(4, 4, 4, 4);
            pStyle.margin = new RectOffset(4, 4, 4, 8);
            GUILayout.BeginHorizontal(pStyle);
            base.OnGUI();
            GUILayout.EndHorizontal();
        }
    }
}
