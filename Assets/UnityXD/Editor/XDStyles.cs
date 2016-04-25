using System;
using UnityEditor;
using UnityEngine;

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

        public Color Normal = EditorGUIUtility.isProSkin ? Color.white : Color.black;
        public Color Disabled = Color.grey;
        public Color Highlight = Color.green;
        public Color Background = GUI.backgroundColor;
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
            Field = new GUIStyle(EditorStyles.textField);
            Field.alignment = TextAnchor.MiddleLeft;
            Field.normal.textColor = Normal;
            Field.normal.background = XDUtility.CreateColoredTexture(Skin);
            Field.focused.textColor = Normal;
            Field.focused.background = XDUtility.CreateColoredTexture(Highlight.Alpha(0.2f));
            Field.border = new RectOffset(4, 0, 0, 0);
            Field.margin = new RectOffset(4, 4, 4, 4);

            // Label 
            Label = new GUIStyle(Field);

            Horiz_Small = new GUIStyle(Field);
            Horiz_Small.fixedWidth = (int) XDSizes.Small*3;
            Horiz_Small.fixedHeight = (int) XDSizes.Small;

            Horiz_Medium = new GUIStyle(Field);
            Horiz_Medium.fixedWidth = (int) XDSizes.Medium*3;
            Horiz_Medium.fixedHeight = (int) XDSizes.Small;

            Horiz_Large = new GUIStyle(Field);
            Horiz_Large.fixedWidth = (int) XDSizes.Large*3;
            Horiz_Large.fixedHeight = (int) XDSizes.Small;
        }
    }
}