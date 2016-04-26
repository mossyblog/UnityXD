using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Sprites;
using UnityEngine;
using UnityXD.Styles;

namespace UnityXD.Editor
{
    public static class XDGUIUtility
    {

        public static void CreateHeading(string label)
        {
            using (new XDGUILayout(false))
            {
                GUILayout.Space(0);
                GUILayout.Label(label, XDGUIStyles.Instance.Heading);
            }
        }
        public static void Bind<T>(ref T inbound, ref T outbound)
        {
            if (GUI.changed)
            {
                inbound = outbound;
            }
            else
            {
                outbound = inbound;
            }
        }

        public static Rect CreateSpacer()
        {
            var rect = new Rect(0,0,0,0);

            using (var layoutRect = new XDGUILayout())
            {
                CreateSpacer(8);
                rect = new Rect(layoutRect.Rect);
            }
            return rect;
        }

        public static void CreateSpacer(int amount)
        {
            GUILayout.Space(amount);
        }

        public static void CreateTabBar(string[] labels, ref string selected)
        {
            var x = 0;
            var style = XDGUIStyles.Instance.Tab;
            var rectBar = CreateEmptyPlaceHolder(labels.Length*(int)style.fixedWidth, (int)style.fixedHeight);

            foreach (var label in labels)
            {
            
                var tabRect = rectBar;
                var isSelected = selected.ToLower().Equals(label.ToLower());
                tabRect.width = style.fixedWidth;
                tabRect.height = style.fixedHeight;
                tabRect.x += x;
                var currSelected = CreateTab(tabRect, label, isSelected);
                if (currSelected)
                    selected = label;
                x += (int)tabRect.width;
            }

        }
        public static bool CreateTab(Rect pos, string label, bool selected)
        {
            var tabSelected = XDGUIStyles.Instance.TabSelectedBackground;
            var tabUnselected = XDGUIStyles.Instance.TabUnselcedBackground;

            var styleAdjusted = new GUIStyle(XDGUIStyles.Instance.Tab);
            styleAdjusted.fixedHeight = pos.height - (styleAdjusted.margin.bottom + styleAdjusted.margin.top);
            styleAdjusted.fixedWidth = pos.width - (styleAdjusted.margin.left + styleAdjusted.margin.right);
            

            var tabStyleAdjusted = new GUIStyle(XDGUIStyles.Instance.ButtonLabel);
            tabStyleAdjusted.normal.textColor = selected
                ? XDGUIStyles.Instance.TabSelected
                : XDGUIStyles.Instance.TabUnselected;
            tabStyleAdjusted.fontStyle = selected ? FontStyle.Bold : FontStyle.Normal;
            

            pos.width = styleAdjusted.fixedWidth;
            pos.height = styleAdjusted.fixedHeight;
            pos.x += styleAdjusted.margin.left;
            pos.y += styleAdjusted.margin.top;

            // Draw the Rect.
            DrawRect(pos, selected ? tabSelected : tabUnselected);

             // Draw the Top Decal.
            if (selected)
            {
                var decalRect = pos;
                decalRect.height = styleAdjusted.border.top;
                DrawRect(decalRect, XDGUIStyles.Instance.Highlight);
            }

            // Render the Label.
            if (GUI.Button(pos, label, tabStyleAdjusted))
            {
                return true;
            }
            return false;
        }

        public static void CreateSwatch(Color color, int size, bool selected)
        {
            var swatchRect = CreateEmptyPlaceHolder(size,size);           
            DrawRect(swatchRect, selected ? XDGUIStyles.Instance.SelectedBorder : XDGUIStyles.Instance.UnselectedBorder, color,
                new RectOffset(1, 1, 1, 1));
        }

        public static void CreateSwatchRow(string[] colors, int size, ref XDColors colorSelected)
        {
            var colorList = new List<XDColors>();
            foreach (var colorName in colors)
            {
                var themeColor = (XDColors)Enum.Parse(typeof(XDColors), colorName);
				colorList.Add(themeColor);
            }

            CreateSwatchRow(colorList.ToArray(),size,ref colorSelected);
        }

        public static void CreateSwatchRow(XDColors[] colors, int size, ref XDColors colorSelected)
        {
            var rowWidth = size*colors.Length;
            var swatchesRect = CreateEmptyPlaceHolder(rowWidth, size);
            var x = swatchesRect.x;
            foreach (var color in colors)
            {
                var swatchRect = swatchesRect;
                var selected = (colorSelected == color);
                swatchRect.width = size;
                swatchRect.height = size;
                swatchRect.x = x;
                DrawRect(swatchRect, selected ? XDGUIStyles.Instance.SelectedBorder : XDGUIStyles.Instance.UnselectedBorder, color.ToColor(), new RectOffset(1, 1, 1, 1));
                if (GUI.Button(swatchRect, String.Empty, GUIStyle.none))
                {
                    colorSelected = color;
                }
                x += swatchRect.width;
            }
        }

        /// <summary>
        /// Creates a TextField that allows type of String.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="field"></param>
        /// <param name="fieldSize"></param>
        /// <param name="isHorizontal"></param>
        public static void CreateTextField(String label, ref string field, XDGUISizes fieldSize, bool isHorizontal = true, bool isenabled = true, TextAnchor labelAlignment = TextAnchor.MiddleLeft)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal, labelAlignment);
            var fieldStyle = new GUIStyle(XDGUIStyles.Instance.Field);
            fieldStyle.alignment = isHorizontal ? TextAnchor.MiddleLeft : TextAnchor.MiddleCenter;

            GUI.enabled = isenabled;
            field = EditorGUI.TextField(fieldRect, field, fieldStyle);
            GUI.enabled = true;
            if (!isHorizontal)
            {
                fieldRect.width -= (int)fieldSize / 2;
                fieldRect.x += (int)fieldSize / 4;
            }
            DrawLine(fieldRect, 1, XDGUIStyles.Instance.Divider, XDVerticalAlignment.Bottom);
        }

        public static void CreateTextField(String label, ref string field, int fieldSize, bool isHorizontal = true, bool isenabled = true, TextAnchor labelAlignment = TextAnchor.MiddleLeft)
        {
            var fieldRect = CreateLabelPair(label, XDGUISizes.Large, isHorizontal, labelAlignment);
            var fieldStyle = new GUIStyle(XDGUIStyles.Instance.Field);
            fieldStyle.alignment = isHorizontal ? TextAnchor.MiddleLeft : TextAnchor.MiddleCenter;

            fieldRect.width = fieldSize;

            GUI.enabled = isenabled;
            field = EditorGUI.TextField(fieldRect, field, fieldStyle);
            GUI.enabled = true;
            
            if (!isHorizontal)
            {
                fieldRect.width -= (int)fieldSize / 2;
                fieldRect.x += (int)fieldSize / 4;
            }
            DrawLine(fieldRect, 1, XDGUIStyles.Instance.Divider, XDVerticalAlignment.Bottom);
        }

        /// <summary>
        /// Creates a TextField that allows type of Double.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="field"></param>
        /// <param name="fieldSize"></param>
        /// <param name="isHorizontal"></param>
        public static void CreateTextField(String label, ref double field, XDGUISizes fieldSize, bool isHorizontal = true, bool isenabled = true, TextAnchor labelAlignment = TextAnchor.MiddleLeft)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal, labelAlignment);
            var fieldStyle = new GUIStyle(XDGUIStyles.Instance.Field);
            fieldStyle.alignment = isHorizontal ? TextAnchor.MiddleLeft : TextAnchor.MiddleCenter;

            GUI.enabled = isenabled;
            field = EditorGUI.DoubleField(fieldRect, field, fieldStyle);
            GUI.enabled = true;
            if (!isHorizontal)
            {
                fieldRect.width -= (int)fieldSize / 2;
                fieldRect.x += (int)fieldSize / 4;
            }
            DrawLine(fieldRect, 1, XDGUIStyles.Instance.Divider, XDVerticalAlignment.Bottom);
        }

        /// <summary>
        /// Creates a TextField that allows type of Int.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="field"></param>
        /// <param name="fieldSize"></param>
        /// <param name="isHorizontal"></param>
        public static void CreateTextField(String label, ref int field, XDGUISizes fieldSize, bool isHorizontal = true, bool isenabled = true, TextAnchor labelAlignment = TextAnchor.MiddleLeft)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal, labelAlignment);
            var fieldStyle = new GUIStyle(XDGUIStyles.Instance.Field);
            fieldStyle.alignment = isHorizontal ? TextAnchor.MiddleLeft : TextAnchor.MiddleCenter;

            GUI.enabled = isenabled;
            field = EditorGUI.IntField(fieldRect, field, fieldStyle);
            GUI.enabled = true;

            if (!isHorizontal)
            {
                fieldRect.width -= (int)fieldSize/2;
                fieldRect.x += (int)fieldSize / 4;
            }
            DrawLine(fieldRect, 1, XDGUIStyles.Instance.Divider, XDVerticalAlignment.Bottom);
        }

        /// <summary>
        /// Creates a Checkbox that allows type of Bool.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="field"></param>
        /// <param name="fieldSize"></param>
        /// <param name="isHorizontal"></param>
        public static void CreateCheckbox(String label, ref bool field, XDGUISizes fieldSize, bool isHorizontal = true, bool isenabled = true, TextAnchor labelAlignment = TextAnchor.MiddleLeft)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal);
            var checkBoxRec = fieldRect;
            var style = XDGUIStyles.Instance.Checkbox;
            var fillEnabled = style.active.background.GetPixel(1, 1);
            var fillDisabled = style.normal.background.GetPixel(1, 1);
            checkBoxRec.width = style.fixedWidth;
            checkBoxRec.height = style.fixedHeight;

            checkBoxRec.y += fieldRect.height/2 - checkBoxRec.height/2;

            DrawRect(checkBoxRec, Color.black, (field ? fillEnabled : fillDisabled), style.border);
            GUI.enabled = isenabled;
            if (GUI.Button(fieldRect, String.Empty, GUIStyle.none))
            {
                field = !field;
            }
            GUI.enabled = true;
            //field = EditorGUI.Toggle(fieldRect, field, XDGUIStyles.Instance.Field);
        }

        public static void CreateEnumField<T>(String label, ref T field, int selected, int fieldSize, string[] filters, bool isHorizontal = true, bool isenabled = true, TextAnchor labelAlignment = TextAnchor.MiddleLeft)
        {
            var fieldRect = CreateLabelPair(label, XDGUISizes.Large, isHorizontal, labelAlignment);
            var rawValues = new Dictionary<Type, int[]>();
            var names = Enum.GetNames(typeof(T));
            var values = GetEnumValues<T>(rawValues);

            var fieldStyle = new GUIStyle(XDGUIStyles.Instance.Field);
            fieldStyle.alignment = isHorizontal ? TextAnchor.MiddleLeft : TextAnchor.MiddleCenter;

            fieldRect.width = fieldSize;

            if (filters != null)
            {
                names = filters;
                var valList = new List<int>();
                foreach (var name in names)
                {
                    valList.Add((int)Enum.Parse(typeof(T), name));
                }
                values = valList.ToArray();
            }
            GUI.enabled = isenabled;
            var selectedIndex = EditorGUI.Popup(fieldRect, Array.IndexOf(values, selected), names, fieldStyle);
            GUI.enabled = true;
            var result = selectedIndex >= 0 && selectedIndex < values.Length ? values[selectedIndex] : selected;
            field = (T)(object)result;

            if (!isHorizontal)
            {
                fieldRect.width -= (int)fieldSize / 2;
                fieldRect.x += (int)fieldSize / 4;
            }
            DrawLine(fieldRect, 1, XDGUIStyles.Instance.Divider, XDVerticalAlignment.Bottom);

        }
        private static int[] GetEnumValues<T>(Dictionary<Type, int[]> enumcache)
        {
            if (!enumcache.ContainsKey(typeof(T)))
                enumcache[typeof(T)] = Enum.GetValues(typeof(T)).Cast<int>().ToArray();
            return enumcache[typeof(T)];
        }

        public static bool CreateButton(string label, XDGUISizes fieldSizes)
        {
            var groupStyle = XDGUIStyles.Instance.ResolveGroup(fieldSizes, true);
            var style = XDGUIStyles.Instance.Button;

            using (var box = new XDGUILayout(false, groupStyle))
            {
                GUILayout.Space(0);
                var buttonRect = box.Rect;
                var thickness = XDGUIStyles.Instance.Button.border;

                buttonRect.height = style.fixedHeight;
                DrawRect(buttonRect, Color.black);

                var fillRect = buttonRect;
                fillRect.width -= thickness.left + thickness.right;
                fillRect.height -= thickness.top + thickness.bottom;
                fillRect.x += thickness.left;
                fillRect.y += thickness.top;

                var buttonStyleAdjusted = new GUIStyle(style);
                buttonStyleAdjusted.fixedHeight = fillRect.height;
                buttonStyleAdjusted.fixedWidth = fillRect.width;

                return GUI.Button(fillRect, "test", buttonStyleAdjusted);
            }
        }

        public static bool CreateButton(Sprite sprite, int width, int height, bool isEnabled = true)
        {
            GUI.enabled = isEnabled;
           return  CreateButton(sprite, width, height, XDGUIStyles.Instance.Button);
        }

        public static bool CreateButton(Sprite sprite, int width, int height, GUIStyle style, bool isBorderEnabled = true, bool isEnabled = true)
        {
            var fillRect = CreateEmptyPlaceHolder(width, height);
           
            if (isBorderEnabled)
                DrawRect(fillRect, Color.black);

            var buttonStyleAdjusted = new GUIStyle(style);
            buttonStyleAdjusted.fixedHeight = fillRect.height;
            buttonStyleAdjusted.fixedWidth = fillRect.width;
            buttonStyleAdjusted.alignment = TextAnchor.MiddleCenter;
            GUI.enabled = isEnabled;
            return GUI.Button(fillRect, SpriteUtility.GetSpriteTexture(sprite, false), buttonStyleAdjusted);
        }

        public static Rect DrawRect(Rect pos, Color color)
        {
            EditorGUI.DrawRect(pos, color);
            return pos;
        }
        public static Rect DrawRect(Rect pos, Color borderColor, Color fillColor, RectOffset thickness)
        {
            // Draw the Border.
            DrawRect(pos, borderColor);

            var fillRect = pos;
            fillRect.width -= thickness.left + thickness.right;
            fillRect.x += thickness.left;
            fillRect.height -= thickness.top + thickness.bottom;
            fillRect.y += thickness.top;

            // Draw the Fill.
            DrawRect(fillRect, fillColor);

            return fillRect;
        }

        public static Rect DrawLine(Rect pos, int thickness, Color lineColor, XDVerticalAlignment alignment)
        {
            
            switch (alignment)
            {
                case XDVerticalAlignment.Top:
                    pos.height = thickness;
                    break;

                case XDVerticalAlignment.Center:
                    pos.y += pos.height/2 - thickness/2;
                    pos.height = thickness;                    
                    break;

                case XDVerticalAlignment.Bottom:
                    pos.y += pos.height - thickness;
                    pos.height = thickness;
                    break;
            }

            DrawRect(pos, lineColor);
            return pos;
        }

        public static void CreateDivider(int width)
        {
            var offset = 8;
            var w = width - offset;
            var rect = XDGUIUtility.CreateEmptyPlaceHolder(w, 8, false);
            rect.x += offset;

            DrawLine(rect, 1, XDGUIStyles.Instance.Divider, XDVerticalAlignment.Center);
        }

        public static Rect CreateLabelPair(string label, XDGUISizes fieldSize, bool isHorizontal = true, TextAnchor alignmnet = TextAnchor.MiddleLeft)
        {
            var groupStyle = XDGUIStyles.Instance.ResolveGroup(fieldSize, isHorizontal);
            var labelRect = new Rect(0, 0, 0, 0);
            var fieldRect = new Rect(0, 0, 0, 0);

            using (var box = new XDGUILayout(isHorizontal, groupStyle))
            {
                GUILayout.Space(0);
                labelRect = box.Rect;
                fieldRect = box.Rect;
                var labelW = box.Rect.width/2;
                var labelH = box.Rect.height/2;
                var labelStyle = new GUIStyle(XDGUIStyles.Instance.Label);
                labelStyle.alignment = alignmnet;
                if (isHorizontal)
                {
                    labelRect.width = labelW;
                    fieldRect.width -= labelW;
                    fieldRect.x += labelW;
                }
                else
                {
                    fieldRect.height = labelH;
                }
                
                
                GUI.Label(labelRect, label, labelStyle);
            }

            return fieldRect;
        }

		public static void CreateSpritePreview(Rect pos, Sprite sprite) {
			GUI.DrawTexture(pos, SpriteUtility.GetSpriteTexture(sprite,false));
		}

        public static Rect CreateEmptyPlaceHolder(int width, int height, bool isHorizontal = true)
        {
            var fieldRect = new Rect(0, 0, width, height);
            var style = new GUIStyle(GUIStyle.none);
            style.fixedWidth = width;
            style.fixedHeight = height;
            style.alignment = TextAnchor.MiddleCenter;
            
            using (var box = new XDGUILayout(isHorizontal, style, GUILayout.Height(height), GUILayout.Width(width)))
            {
                GUILayout.Space(0);
                fieldRect = box.Rect;
            }

            return fieldRect;
        }

        public static Texture2D CreateColoredTexture(Color col)
        {
            var width = 10;
            var height = 10;
            var pix = new Color[width*height];

            for (var i = 0; i < pix.Length; i++)
                pix[i] = col;

            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        public static Color Alpha(this Color clr, float amt)
        {
            clr.a = amt;
            return clr;
        }

        public static void DebugLastRect(bool outputConsole = false)
        {
            var rect = GUILayoutUtility.GetLastRect();
            var debugColor = Color.red.Alpha(0.2F);
            EditorGUI.DrawRect(rect, debugColor);

            if (outputConsole)
            {
                Debug.LogFormat("W:{0} H:{1} X:{2} Y:{3}", rect.width, rect.height, rect.x, rect.y);
            }
        }
    }
}