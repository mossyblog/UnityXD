using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Styles;

namespace UnityXD.XDGUIEditor
{
    public class XDGUIAnchorInspector
    {

        public GUIStyle PanelStyle = XDGUIStyles.Instance.Panel;
        private int FieldHeight = 22;
        private UIComponent _componentRef;

        public static XDGUIAnchorInspector Create(ref UIComponent componentRef)
        {
            return new XDGUIAnchorInspector(ref componentRef);            
        }

        public XDGUIAnchorInspector(ref UIComponent componentRef)
        {
            _componentRef = componentRef;
        }
        
        public XDGUIAnchorInspector Metrics()
        {
    
            var isHeightDependentOnWidth = _componentRef.IsHeightDependantOnWidth;
            var isHStretched = _componentRef.IsHorizontalStretchEnabled;
            var isVStretched = _componentRef.IsVeritcalStretchEnabled;

            // SWITCHES.
            var wEnabled = _componentRef.CurrentStyle.Size == XDSizes.Custom && !isHStretched;
            var hEnabled = _componentRef.CurrentStyle.Size == XDSizes.Custom && !isVStretched && !isHeightDependentOnWidth;
            var linkEnabled = wEnabled && !isHStretched && !isVStretched;

            using (new XDGUIPanel(false, PanelStyle))
            {
                XDGUI.Create().Label("Size").Size(64, FieldHeight, 64).RenderEnumField(ref _componentRef.CurrentStyle.Size, null, true);
            }

            using (new XDGUIPanel(true, PanelStyle))
            {
                using (new XDGUIPanel(false, GUILayout.MaxWidth(64)))
                {
                    
                    XDGUI.Create().Label("W").Size(16, FieldHeight, 48).RenderTextField(ref _componentRef.Width, true, wEnabled);
                    EditorGUILayout.Space();
                    XDGUI.Create().Label("H").Size(16, FieldHeight, 48).RenderTextField(ref _componentRef.Height, true, hEnabled);                    
                }
                

                using (new XDGUIPanel(false, GUILayout.Width(24)))
                {
                    GUILayout.Space(8);
                    var isLinkSprite = Resources.Load<Sprite>("Icons/Editor/SizeLink_" + (!isHeightDependentOnWidth ? "On" : "Off"));
                    XDGUI.Create()
                        .Sprite(isLinkSprite)
                        .Size(24, 48)
                        .Style(GUIStyle.none)
                        .RenderButton(ref _componentRef.IsHeightDependantOnWidth, linkEnabled);
                }

                GUILayout.FlexibleSpace();

                using (new XDGUIPanel(false))
                {
                    XDGUI.Create().Label("X").Size(16, FieldHeight, 48).RenderTextField(ref _componentRef.X, true);
                    EditorGUILayout.Space();
                    XDGUI.Create().Label("Y").Size(16, FieldHeight, 48).RenderTextField(ref _componentRef.Y, true);
                }
            }
            return this;
        }

        public XDGUIAnchorInspector AnchorToolBar()
        {
            var horizontalAlignment = _componentRef.CurrentAnchorAlignment.ToHorizontalAlignment();
            var vertAlignment = _componentRef.CurrentAnchorAlignment.ToVerticalAlignment();

            var rightEnabled = XDHorizontalAlignment.Right == horizontalAlignment;
            var leftEnabled = XDHorizontalAlignment.Left == horizontalAlignment;
            var middleHEnabled = XDHorizontalAlignment.Center == horizontalAlignment;
            var topEnabled = XDVerticalAlignment.Top == vertAlignment;
            var middleVEnabled = XDVerticalAlignment.Center == vertAlignment;
            var bottomEnabled = XDVerticalAlignment.Bottom == vertAlignment;
            var horizontalStretchEnabled = _componentRef.IsHorizontalStretchEnabled;
            var isVeritcalStretchEnabled = _componentRef.IsVeritcalStretchEnabled;

            var path = "icons/Editor/";
            var horizPrefix = "AlignHoriz";
            var vertPrefix = "AlignVert";
            var onState = "On";
            var offState = "Off";

            var leftName = "Left_" + (leftEnabled ? onState : offState);
            var rightName = "Right_" + (rightEnabled ? onState : offState);
            var topName = "Top_" + (topEnabled ? onState : offState);
            var botName = "Bottom_" + (bottomEnabled ? onState : offState);
            var midHName = "Middle_" + (middleHEnabled ? onState : offState);
            var midVName = "Middle_" + (middleVEnabled ? onState : offState);

            var leftSprite = Resources.Load<Sprite>(path + horizPrefix + leftName);
            var midHSprite = Resources.Load<Sprite>(path + horizPrefix + midHName);
            var rightSprite = Resources.Load<Sprite>(path + horizPrefix + rightName);

            var topSprite = Resources.Load<Sprite>(path + vertPrefix + topName);
            var midVSprite = Resources.Load<Sprite>(path + vertPrefix + midVName);
            var botSprite = Resources.Load<Sprite>(path + vertPrefix + botName);

            var horizStretchSprite = Resources.Load<Sprite>(path + "StretchHoriz_" + (horizontalStretchEnabled ? onState : offState));
            var vertStretchSprite = Resources.Load<Sprite>(path + "StretchVert_" + (isVeritcalStretchEnabled ? onState : offState));
            const int size = 24;
            var style = new GUIStyle(GUIStyle.none)
            {
                normal = {background = XDGUIUtility.CreateColoredTexture(Color.white)},
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(3, 3, 3, 3)
            };

            using (new XDGUIPanel(true, style, GUILayout.MaxWidth(256)))
            {

                XDGUI.Create().Sprite(horizStretchSprite).Size(size).Style(style).RenderButton(ref horizontalStretchEnabled);
                XDGUI.Create().Sprite(vertStretchSprite).Size(size).Style(style).RenderButton(ref isVeritcalStretchEnabled);

                GUILayout.Space(8);

                if (XDGUI.Create().Sprite(leftSprite).Size(size).Style(style).RenderButton())
                    horizontalAlignment = XDHorizontalAlignment.Left;

                if (XDGUI.Create().Sprite(midHSprite).Size(size).Style(style).RenderButton())
                    horizontalAlignment = XDHorizontalAlignment.Center;

                if (XDGUI.Create().Sprite(rightSprite).Size(size).Style(style).RenderButton())
                    horizontalAlignment = XDHorizontalAlignment.Right;

                GUILayout.Space(8);

                if (XDGUI.Create().Sprite(topSprite).Size(size).Style(style).RenderButton())
                    vertAlignment = XDVerticalAlignment.Top;

                if (XDGUI.Create().Sprite(midVSprite).Size(size).Style(style).RenderButton())
                    vertAlignment = XDVerticalAlignment.Center;

                if (XDGUI.Create().Sprite(botSprite).Size(size).Style(style).RenderButton())
                    vertAlignment = XDVerticalAlignment.Bottom;
            }
            if (GUI.changed)
            {
                var align = XDThemeUtility.ToAlignment(horizontalAlignment, vertAlignment);
                _componentRef.Dock(align, horizontalStretchEnabled, isVeritcalStretchEnabled);
            }

            return this;
        }

        public XDGUIAnchorInspector Padding()
        {
            /*********************************************************************************
                  Padding
              *********************************************************************************/
            XDGUI.Create().Label("Padding").Style(XDGUIStyles.Instance.Heading).RenderLabel();
            using (new XDGUIPanel(true, PanelStyle))
            {
                var left = _componentRef.Padding.left;
                var right = _componentRef.Padding.right;
                var top = _componentRef.Padding.top;
                var bot = _componentRef.Padding.bottom;
                XDGUI.Create().Label("Left").RenderTextField(ref left, false);
                XDGUI.Create().Label("Right").RenderTextField(ref right, false);
                XDGUI.Create().Label("Top").RenderTextField(ref top, false);
                XDGUI.Create().Label("Bottom").RenderTextField(ref bot, false);
                _componentRef.SetPadding(new RectOffset(left, right, top, bot));
            }
            EditorGUILayout.Space();
            return this;
        }

        public XDGUIAnchorInspector Margins()
        {
            /*********************************************************************************
              Margin
          *********************************************************************************/

            XDGUI.Create().Label("Margins").Style(XDGUIStyles.Instance.Heading).RenderLabel();
            using (new XDGUIPanel(true, PanelStyle))
            {
                var left = _componentRef.Margin.left;
                var right = _componentRef.Margin.right;
                var top = _componentRef.Margin.top;
                var bot = _componentRef.Margin.bottom;
                
                XDGUI.Create().Label("Left").RenderTextField(ref left, false);
                XDGUI.Create().Label("Right").RenderTextField(ref right, false);
                XDGUI.Create().Label("Top").RenderTextField(ref top, false);
                XDGUI.Create().Label("Bottom").RenderTextField(ref bot, false);

                _componentRef.Margin = new RectOffset(left, right, top, bot);

            }
            return this;
        }

    }
}
