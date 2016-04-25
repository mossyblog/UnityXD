using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityXD.Styles;

namespace UnityXD.Editor
{
    public class XDGUIStyles
    {
        private GUIStyle Horiz_Small;
        private GUIStyle Horiz_Medium;
        private GUIStyle Horiz_Large;
        private GUIStyle Vert_Small;
        private GUIStyle Vert_Medium;
        private GUIStyle Vert_Large;

        private static XDGUIStyles _instance;

        public GUIStyle Field;
        public GUIStyle Label;
        public GUIStyle Checkbox;
        public GUIStyle Button;
        public GUIStyle ButtonLabel;
        public GUIStyle Tab;
        public GUIStyle Body;

        // Foregrounds
        public Color Normal = EditorGUIUtility.isProSkin ? XDColors.ChromeDarker.ToColor() : XDColors.Chrome.ToColor();
        public Color PressedForeground = XDColors.ChromeLightest.ToColor();
        public Color Disabled = Color.grey;
        public Color Highlight = XDColors.Brand.ToColor(0.1f);
        public Color Selected = XDColors.BrandLighter.ToColor();
        public Color TabSelected = XDColors.Brand.ToColor();
        public Color TabUnselected = XDColors.ChromeLighter.ToColor();
        public Color Divider = Color.black.Alpha(0.2F);

        // Backgrounds.
        public Color Background = XDColors.ChromeLightest.ToColor();
        public Color FieldBackground = XDColors.ChromeLightest.ToColor();
        public Color InputBackground = XDColors.Brand.ToColor();
        public Color PressedBackground = XDColors.Brand.ToColor();
        public Color SelectedBorder = Color.black;
        public Color UnselectedBorder = Color.white;
        public Color TabSelectedBackground = XDColors.ChromeLightest.ToColor();
        public Color TabUnselcedBackground = XDColors.Chrome.ToColor();

        // Other.
        public Color32 Skin = EditorGUIUtility.isProSkin ? new Color32(56, 56, 56, 255) : new Color32(194, 194, 194, 255);

        public static XDGUIStyles Instance
        {
            get { return _instance ?? (_instance = new XDGUIStyles()); }        
        }

        public GUIStyle ResolveGroup(XDGUISizes size, bool isHorizontal)
        {
            switch (size)
            {
                case XDGUISizes.Small:
                    return isHorizontal ? _instance.Horiz_Small : _instance.Vert_Small;
                case XDGUISizes.Medium:
                    return isHorizontal ? _instance.Horiz_Medium : _instance.Vert_Medium;
                case XDGUISizes.Large:
                    return isHorizontal ? _instance.Horiz_Large : _instance.Vert_Large;
                default:
                    throw new ArgumentOutOfRangeException("size", size, null);
            }
        }

        public XDGUIStyles()
        {
            InitStyles();
        }

        public void InitStyles()
        {
            // TextField
            Label = new GUIStyle(EditorStyles.textField);
            Label.alignment = TextAnchor.MiddleLeft;
            Label.normal.textColor = Normal;
            Label.normal.background = XDGUIUtility.CreateColoredTexture(Color.clear);
            Label.focused.textColor = Normal;
            Label.focused.background = XDGUIUtility.CreateColoredTexture(Color.clear);


            // Label 
            Field = new GUIStyle(Label);
            Field.normal.textColor = XDColors.ChromeDarkest.ToColor();
            Field.focused.textColor = XDColors.BrandDarkest.ToColor();
            Field.fontStyle = FontStyle.Bold;
            Field.normal.background = XDGUIUtility.CreateColoredTexture(FieldBackground);
            Field.focused.background = XDGUIUtility.CreateColoredTexture(Highlight);

            // GROUP - Horizontal
            Horiz_Small = new GUIStyle(GUIStyle.none);
            Horiz_Small.fixedWidth = (int)XDGUISizes.Small * 3;
            Horiz_Small.fixedHeight = 24;
            Horiz_Small.margin = new RectOffset(4, 4, 4, 4);

            Horiz_Medium = new GUIStyle(Horiz_Small);
            Horiz_Medium.fixedWidth = (int)XDGUISizes.Medium * 3;
            Horiz_Medium.fixedHeight = 24;

            Horiz_Large = new GUIStyle(Horiz_Medium);
            Horiz_Large.fixedWidth = (int)XDGUISizes.Large * 3;
            Horiz_Large.fixedHeight = 24;

            Checkbox = new GUIStyle();
            Checkbox.fixedWidth = 16;
            Checkbox.fixedHeight = 16;
            Checkbox.normal.background = XDGUIUtility.CreateColoredTexture(Background);
            Checkbox.active.background = XDGUIUtility.CreateColoredTexture(Highlight);
            Checkbox.border = new RectOffset(1, 1, 1, 1);

            Button = new GUIStyle(Checkbox);
            Button.fixedHeight = 24;
            Button.alignment = TextAnchor.MiddleCenter;
            Button.normal.textColor = Normal;
            Button.normal.background = XDGUIUtility.CreateColoredTexture(InputBackground);
            Button.active.textColor = PressedForeground;
            Button.active.background = XDGUIUtility.CreateColoredTexture(PressedBackground);

            ButtonLabel = new GUIStyle(Label);
            ButtonLabel.alignment = TextAnchor.MiddleCenter;

            Tab = new GUIStyle(Button);
            Tab.fixedHeight = 24;
            Tab.fixedWidth = 72;
            Tab.border = new RectOffset(0, 0, 2, 0);
            Tab.normal.textColor = Color.blue;
            Tab.normal.background = XDGUIUtility.CreateColoredTexture(TabUnselected);
            Tab.active.textColor = Color.black;
            Tab.active.background = XDGUIUtility.CreateColoredTexture(TabSelected);
            Tab.margin = new RectOffset(0, 4, 0, 0);


            Body = new GUIStyle(GUIStyle.none);
            Body.padding = new RectOffset(4, 4, 4, 4);
            Body.normal.background = XDGUIUtility.CreateColoredTexture(Background);
        }
    }
}