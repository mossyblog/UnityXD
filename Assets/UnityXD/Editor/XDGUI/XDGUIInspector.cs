using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Styles;
using Object = System.Object;

namespace UnityXD.XDGUIEditor
{
    public class XDGUIInspector
    {
        protected UIComponent _componentRef;
        protected GUIStyle PanelStyle = XDGUIStyles.Instance.Panel;
        protected int FieldSmall = 32;
        protected int FieldMedium = 64;
        protected int FieldLarge = 92;
        protected int FieldXXL = 128;
        protected int LabelSmall = 48;
        protected int LabelMedium = 92;
        protected int LabelLarge = 128;
        protected int FieldHeight = 22;
        protected int CheckBoxSize = 16;

        public XDGUIInspector Context(ref UIComponent component)
        {
            _componentRef = component;
            return this;
        }

        public XDGUIInspector SizeAndPositioning()
        {
            var isHeightDependentOnWidth = _componentRef.IsHeightDependantOnWidth;
            var isHStretched = _componentRef.IsHorizontalStretchEnabled;
            var isVStretched = _componentRef.IsVeritcalStretchEnabled;

            // SWITCHES.
            var wEnabled = _componentRef.CurrentStyle.Size == XDSizes.Custom && !isHStretched;
            var hEnabled = _componentRef.CurrentStyle.Size == XDSizes.Custom && !isVStretched && !isHeightDependentOnWidth;
            var linkEnabled = wEnabled && !isVStretched;


            using (new XDGUIPanel(true, PanelStyle))
            {
                using (new XDGUIPanel(false, GUILayout.MaxWidth(FieldLarge)))
                {
                    XDGUI.Create().Text("W").Size(LabelSmall, FieldHeight, FieldSmall).TextField(ref _componentRef.Width, true, wEnabled);
                    EditorGUILayout.Space();
                    XDGUI.Create().Text("H").Size(LabelSmall, FieldHeight, FieldSmall).TextField(ref _componentRef.Height, true, hEnabled);
                }


                using (new XDGUIPanel(false, GUILayout.Width(24)))
                {
                    GUILayout.Space(8);
                    var isLinkSprite = Resources.Load<Sprite>("Icons/Editor/SizeLink_" + (isHeightDependentOnWidth ? "On" : "Off"));
                    XDGUI.Create()
                        .Sprite(isLinkSprite)
                        .Size(24, 48)
                        .Style(GUIStyle.none)
                        .Button(ref _componentRef.IsHeightDependantOnWidth, linkEnabled);
                }

                GUILayout.FlexibleSpace();

                using (new XDGUIPanel(false))
                {
                    XDGUI.Create().Text("X").Size(LabelSmall, FieldHeight, FieldSmall).TextField(ref _componentRef.X, true);
                    EditorGUILayout.Space();
                    XDGUI.Create().Text("Y").Size(LabelSmall, FieldHeight, FieldSmall).TextField(ref _componentRef.Y, true);
                }
            }
            XDGUI.Create().Divider(XDVerticalAlignment.Center);
            return this;
        }

        public XDGUIInspector AnchorToolbar()
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
                normal = { background = XDGUIUtility.CreateColoredTexture(Color.white) },
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(3, 3, 3, 3)
            };

            using (new XDGUIPanel(true, style, GUILayout.MaxWidth(256)))
            {

                XDGUI.Create().Sprite(horizStretchSprite).Size(size).Style(style).Button(ref horizontalStretchEnabled);
                XDGUI.Create().Sprite(vertStretchSprite).Size(size).Style(style).Button(ref isVeritcalStretchEnabled);

                GUILayout.Space(8);

                if (XDGUI.Create().Sprite(leftSprite).Size(size).Style(style).Button())
                    horizontalAlignment = XDHorizontalAlignment.Left;

                if (XDGUI.Create().Sprite(midHSprite).Size(size).Style(style).Button())
                    horizontalAlignment = XDHorizontalAlignment.Center;

                if (XDGUI.Create().Sprite(rightSprite).Size(size).Style(style).Button())
                    horizontalAlignment = XDHorizontalAlignment.Right;

                GUILayout.Space(8);

                if (XDGUI.Create().Sprite(topSprite).Size(size).Style(style).Button())
                    vertAlignment = XDVerticalAlignment.Top;

                if (XDGUI.Create().Sprite(midVSprite).Size(size).Style(style).Button())
                    vertAlignment = XDVerticalAlignment.Center;

                if (XDGUI.Create().Sprite(botSprite).Size(size).Style(style).Button())
                    vertAlignment = XDVerticalAlignment.Bottom;
            }

            XDGUI.Create().Divider(XDVerticalAlignment.Center);

            if (GUI.changed)
            {
                var align = XDThemeUtility.ToAlignment(horizontalAlignment, vertAlignment);
                _componentRef.Dock(align, horizontalStretchEnabled, isVeritcalStretchEnabled);
            }
            return this;

           
        }

        public XDGUIInspector Swatch(string heading, ref XDColors field)
        {
            XDGUI.Create().SwatchPicker(ref field, heading);
            return this;
        }

        public XDGUIInspector Margin()
        {
            XDGUI.Create().RectOffsetField(ref _componentRef.Margin, "Margin");
            if (GUI.changed)
            {
                _componentRef.SetMargin(_componentRef.Margin);
            }
            return this;
        }

        public XDGUIInspector Padding()
        {
            XDGUI.Create().RectOffsetField(ref _componentRef.Padding, "Padding");
            if (GUI.changed)
            {
                _componentRef.SetPadding(_componentRef.Padding);
            }
            return this;
        }

        public XDGUIInspector Heading(string label)
        {
            EditorGUILayout.Space();

            XDGUI.Create().Text(label).Style(XDGUIStyles.Instance.Heading).Label();

            EditorGUILayout.Space();
            return this;
            
        }

        public XDGUIInspector LabelAlignment(ref TextAnchor field, string prefix) 
        {
            XDGUI.Create()
               .Text(prefix)
               .Size(LabelMedium, FieldHeight, FieldXXL)
               .ComboBox(ref field, null, true);
            EditorGUILayout.Space();
            return this;
        }

        public XDGUIInspector Sprite(string prefix, ref Sprite field)
        {
            XDGUI.Create()
               .Text(prefix)
               .Size(LabelMedium, FieldHeight)
               .SpriteField(ref field, true, false);
            return this;
        }

        public XDGUIInspector Bind<T>(string label, ref T field)
        {
            var arg = field as UIComponent;

            XDGUI.Create()
                .Text(label)
                .Size(LabelMedium, FieldHeight)
                .XDField<T>(ref arg, true);
            field = (T)(Object)arg;
            return this;
        }
       
        public XDGUIInspector Sizing(string label, ref XDSizes field) {
           
            using (new XDGUIPanel(false, XDGUIStyles.Instance.Panel))
            {
                XDGUI.Create().Text(label).Size(LabelSmall, FieldHeight, FieldMedium).ComboBox(ref field, null, true);
            }
            return this;
        }

        public XDGUIInspector Button(string label, ref bool field, XDHorizontalAlignment alignment) {

            using (new XDGUIPanel(true, XDGUIStyles.Instance.Panel))
            {
                if (alignment == XDHorizontalAlignment.Right || alignment == XDHorizontalAlignment.Center)
                {
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.Space();
                }
                XDGUI.Create()
                .Text(label)
                .Size(LabelMedium, FieldHeight)
                .Style(XDGUIStyles.Instance.Button)
                .Button(ref field, true);
                
                EditorGUILayout.Space();
                if (alignment == XDHorizontalAlignment.Center)
                {
                    GUILayout.FlexibleSpace();
                }
            }
            return this;
        }

        public XDGUIInspector Icon(ref XDIcons iconField, ref XDVerticalAlignment iconAlignment)
        {
            using (new XDGUIPanel(false, XDGUIStyles.Instance.Panel))
            {
                using (new XDGUIPanel(true))
                {
                    XDGUI.Create().Text("Icon").Size(LabelMedium, FieldHeight, FieldLarge).ComboBox(ref iconField, null, true);
                    XDGUI.Create().Icon(ref iconField, XDSizes.XS);
                }
                EditorGUILayout.Space();
                XDGUI.Create().Text("Placement").Size(LabelMedium, FieldHeight, FieldMedium).ComboBox(ref iconAlignment, null, true);                
            }
            return this;
        }


        public XDGUIInspector Icon(ref XDIcons iconField)
        {
            XDGUI.Create().Text("Icon").Size(LabelMedium, FieldHeight, FieldLarge).ComboBox(ref iconField, null, true);
            return this;
        }

        public XDGUIInspector TextField(ref String field, string prefix)
        {
            using (new XDGUIPanel(false, XDGUIStyles.Instance.Panel))
            {
                XDGUI.Create()
               .Text(prefix)
               .Size(LabelMedium, FieldHeight)
               .TextField(ref field, true);
            }
            return this;
        }

        public XDGUIInspector TextField(ref int field, string prefix)
        {
            XDGUI.Create()
                .Text(prefix)
                .Size(LabelMedium, FieldHeight)
                .TextField(ref field, true);
            return this;
        }

        public XDGUIInspector TextField(ref double field, string prefix)
        {
            XDGUI.Create()
                .Text(prefix)
                .Size(LabelMedium, FieldHeight)
                .TextField(ref field, true);
            return this;
        }

        public XDGUIInspector CheckBox(string prefix, ref bool isSelected)
        {
            XDGUI.Create()
                .Text(prefix)
                .Size(LabelMedium, FieldHeight,CheckBoxSize,CheckBoxSize)
                .CheckBox(ref isSelected, true);
            return this;
        }

    }
}
