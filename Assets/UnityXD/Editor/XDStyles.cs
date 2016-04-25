using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UnityXD.Editor
{
    public class XDStyles
    {
        private GUIStyle Horiz_Small;
        private GUIStyle Horiz_Medium;
        private GUIStyle Horiz_Large;
        private GUIStyle Vert_Small;
        private GUIStyle Vert_Medium;
        private GUIStyle Vert_Large;

        private static XDStyles _instance;

        public GUIStyle Field;
        public GUIStyle Label;
        public GUIStyle Checkbox;
        public GUIStyle Button;
        public GUIStyle ButtonLabel;
        public GUIStyle Tab;

        // Foregrounds
        public Color Normal = EditorGUIUtility.isProSkin ? Color.white : Color.black;
        public Color PressedForeground = Color.white;
        public Color Disabled = Color.grey;
        public Color Highlight = Color.green;
        public Color Selected = Color.blue;
        public Color TabSelected = Color.white;
        public Color TabUnselected = Color.gray;

        // Backgrounds.
        public Color Background = GUI.backgroundColor;
        public Color FieldBackground = Color.white;
        public Color InputBackground = Color.gray;
        public Color PressedBackground = Color.green;
        public Color SelectedBorder = Color.black;
        public Color UnselectedBorder = Color.white;
        
        // Other.
        public Color32 Skin = EditorGUIUtility.isProSkin ? new Color32(56, 56, 56, 255) : new Color32(194, 194, 194, 255);

        public static XDStyles Instance
        {
            get { return _instance ?? (_instance = new XDStyles()); }        
        }

        public GUIStyle ResolveGroup(XDSizes size, bool isHorizontal)
        {
            switch (size)
            {
                case XDSizes.Small:
                    return isHorizontal ? _instance.Horiz_Small : _instance.Vert_Small;
                case XDSizes.Medium:
                    return isHorizontal ? _instance.Horiz_Medium : _instance.Vert_Medium;
                case XDSizes.Large:
                    return isHorizontal ? _instance.Horiz_Large : _instance.Vert_Large;
                default:
                    throw new ArgumentOutOfRangeException("size", size, null);
            }
        }

        public XDStyles()
        {
            // TextField
            Label = new GUIStyle(EditorStyles.textField);
            Label.alignment = TextAnchor.MiddleLeft;
            Label.normal.textColor = Normal;
            Label.normal.background = XDUtility.CreateColoredTexture(Color.clear);
            Label.focused.textColor = Normal;
            Label.focused.background = XDUtility.CreateColoredTexture(Color.clear);
            Label.border = new RectOffset(4, 0, 0, 0);
            Label.margin = new RectOffset(4, 4, 4, 4);
            

            // Label 
            Field = new GUIStyle(Label);
            Field.normal.background = XDUtility.CreateColoredTexture(FieldBackground);
            Field.focused.background = XDUtility.CreateColoredTexture(Highlight);

            // GROUP - Horizontal
            Horiz_Small = new GUIStyle(GUIStyle.none);
            Horiz_Small.fixedWidth = (int) XDSizes.Small*3;
            Horiz_Small.fixedHeight = 24;
            Horiz_Small.margin = new RectOffset(4,4,4,4);

            Horiz_Medium = new GUIStyle(Horiz_Small);
            Horiz_Medium.fixedWidth = (int) XDSizes.Medium*3;
            Horiz_Medium.fixedHeight = 24;

            Horiz_Large = new GUIStyle(Horiz_Medium);
            Horiz_Large.fixedWidth = (int) XDSizes.Large*3;
            Horiz_Large.fixedHeight = 24;

            Checkbox = new GUIStyle();
            Checkbox.fixedWidth = 16;
            Checkbox.fixedHeight = 16;            
            Checkbox.normal.background = XDUtility.CreateColoredTexture(Background);
            Checkbox.active.background = XDUtility.CreateColoredTexture(Highlight);
            Checkbox.border = new RectOffset(1,1,1,1);

            Button = new GUIStyle(Checkbox);
            Button.fixedHeight = 24;
            Button.alignment = TextAnchor.MiddleCenter;
            Button.normal.textColor = Normal;           
            Button.normal.background = XDUtility.CreateColoredTexture(InputBackground);
            Button.active.textColor = PressedForeground;
            Button.active.background = XDUtility.CreateColoredTexture(PressedBackground);

            ButtonLabel = new GUIStyle(Label);
            ButtonLabel.alignment = TextAnchor.MiddleCenter;

            Tab = new GUIStyle(Button);
            Tab.fixedHeight = 24;
            Tab.fixedWidth = 72;
            Tab.border = new RectOffset(0,0,2,0);
            Tab.normal.textColor = Color.blue;
            Tab.normal.background = XDUtility.CreateColoredTexture(TabUnselected);
            Tab.active.textColor = Color.black;
            Tab.active.background = XDUtility.CreateColoredTexture(TabSelected);
            Tab.margin = new RectOffset(0,4,0,0);
        }
    }
}