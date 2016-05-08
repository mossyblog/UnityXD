using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.Styles;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDComponents
{
    public interface IXDLayoutMetrics : IXDWidget
    {
        IXDLayoutMetrics BindTo(ref BaseControl control);
    }

    internal class XDLayoutMetrics : XDWidget, IXDLayoutMetrics
    {
        private BaseControl baseControl;

        public IXDLayoutMetrics BindTo(ref BaseControl control)
        {
            baseControl = control;

            var pStyle = new GUIStyle();
            pStyle.normal.textColor = ColorLibrary.ChromeLighter.ToColor();
            pStyle.normal.background = CreateColoredTexture(ColorLibrary.ChromeDark.ToColor());
            pStyle.margin = new RectOffset(16,16,16,16);
            pStyle.padding = new RectOffset(8,8,8,8);
            
            var lableStyle = new GUIStyle(GUI.skin.label);
            lableStyle.normal.textColor = ColorLibrary.ChromeLighter.ToColor();
            lableStyle.fontStyle = FontStyle.Bold;
            lableStyle.fixedWidth = 32;
            lableStyle.alignment= TextAnchor.MiddleRight;

            var fieldGroupStyle = new GUIStyle {padding = new RectOffset(4, 4, 4, 4)};

            parent.
                VerticalLayout(pStyle)
                .HorizontalLayout(fieldGroupStyle)
                .TextBox().Field((s) => { baseControl.Width = s; }, baseControl.Width).Label("W",lableStyle).Width(32).End()
                .FlexiSpace()
                .TextBox().Field((s) => { baseControl.X = s; }, baseControl.X).Label("X", lableStyle).Width(32).End()
                .FlexiSpace()
                .End()
                .HorizontalLayout(fieldGroupStyle)
                .TextBox().Field((s) => { baseControl.Height = s; }, baseControl.Height).Label("H", lableStyle).Width(32).End()
                .FlexiSpace()
                .TextBox().Field((s) => { baseControl.Y = s; }, baseControl.Y).Label("Y", lableStyle).Width(32).End()
                .FlexiSpace()
                .End()
               .End();

            return this;
        }

        public XDLayoutMetrics(IXDLayout parent) : base(parent)
        {
        }

        public override void Render()
        {
        }
    }
}
