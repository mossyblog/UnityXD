using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityXD.Editor
{
    public static class XDUtility
    {
        public static void CreateSwatch(Color color, int size, bool selected)
        {
            var swatchRect = CreateEmptyPlaceHolder(size,size);           
            DrawRect(swatchRect, selected ? XDStyles.Instance.SelectedBorder : XDStyles.Instance.UnselectedBorder, color,
                new RectOffset(1, 1, 1, 1));
        }

        public static void CreateSwatchRow(Color[] colors, int size, Color colorSelected)
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
                DrawRect(swatchRect, selected ? XDStyles.Instance.SelectedBorder : XDStyles.Instance.UnselectedBorder, color,
                    new RectOffset(1, 1, 1, 1));

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
        public static void CreateTextField(String label, ref string field, XDSizes fieldSize, bool isHorizontal = true)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal);
            field = EditorGUI.TextField(fieldRect, field, XDStyles.Instance.Field);
        }

        /// <summary>
        /// Creates a TextField that allows type of Double.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="field"></param>
        /// <param name="fieldSize"></param>
        /// <param name="isHorizontal"></param>
        public static void CreateTextField(String label, ref double field, XDSizes fieldSize, bool isHorizontal = true)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal);
            field = EditorGUI.DoubleField(fieldRect, field, XDStyles.Instance.Field);
        }

        /// <summary>
        /// Creates a TextField that allows type of Int.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="field"></param>
        /// <param name="fieldSize"></param>
        /// <param name="isHorizontal"></param>
        public static void CreateTextField(String label, ref int field, XDSizes fieldSize, bool isHorizontal = true)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal);
            field = EditorGUI.IntField(fieldRect, field, XDStyles.Instance.Field);
        }

        /// <summary>
        /// Creates a Checkbox that allows type of Bool.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="field"></param>
        /// <param name="fieldSize"></param>
        /// <param name="isHorizontal"></param>
        public static void CreateCheckbox(String label, ref bool field, XDSizes fieldSize, bool isHorizontal = true)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal);
            var checkBoxRec = fieldRect;
            var style = XDStyles.Instance.Checkbox;
            var fillEnabled = style.active.background.GetPixel(1, 1);
            var fillDisabled = style.normal.background.GetPixel(1, 1);
            checkBoxRec.width = style.fixedWidth;
            checkBoxRec.height = style.fixedHeight;

            checkBoxRec.y += fieldRect.height/2 - checkBoxRec.height/2;

            DrawRect(checkBoxRec, Color.black, (field ? fillEnabled : fillDisabled), style.border);

            if (GUI.Button(fieldRect, String.Empty, GUIStyle.none))
            {
                field = !field;
            }
            //field = EditorGUI.Toggle(fieldRect, field, XDStyles.Instance.Field);
        }

        public static bool CreateButton(string label, XDSizes fieldSizes)
        {
            var groupStyle = XDStyles.Instance.ResolveGroup(fieldSizes, true);
            var style = XDStyles.Instance.Button;

            using (var box = new XDLayout(false, groupStyle))
            {
                GUILayout.Space(0);
                var buttonRect = box.Rect;
                var thickness = XDStyles.Instance.Button.border;

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

        public static void CreateEnumField<T>(String label, ref T field, int selected, XDSizes fieldSize, string[] filters, bool isHorizontal = true)
        {
            var fieldRect = CreateLabelPair(label, fieldSize, isHorizontal);
            var rawValues = new Dictionary<Type, int[]>();
            var names = Enum.GetNames(typeof (T));
            var values = GetEnumValues<T>(rawValues);

            if (filters != null)
            {
                names = filters;
                var valList = new List<int>();
                foreach (var name in names)
                {
                    valList.Add((int) Enum.Parse(typeof (T), name));
                }
                values = valList.ToArray();
            }
            var selectedIndex = EditorGUI.Popup(fieldRect, Array.IndexOf(values, selected), names, XDStyles.Instance.Field);
            var result = selectedIndex >= 0 && selectedIndex < values.Length ? values[selectedIndex] : selected;
            field = (T) (object) result;
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

        private static int[] GetEnumValues<T>(Dictionary<Type, int[]> enumcache)
        {
            if (!enumcache.ContainsKey(typeof (T)))
                enumcache[typeof (T)] = Enum.GetValues(typeof (T)).Cast<int>().ToArray();
            return enumcache[typeof (T)];
        }

        public static Rect CreateLabelPair(string label, XDSizes fieldSize, bool isHorizontal = true)
        {
            var groupStyle = XDStyles.Instance.ResolveGroup(fieldSize, isHorizontal);
            var labelRect = new Rect(0, 0, 0, 0);
            var fieldRect = new Rect(0, 0, 0, 0);


            using (var box = new XDLayout(isHorizontal, groupStyle))
            {
                GUILayout.Space(0);
                labelRect = box.Rect;
                fieldRect = box.Rect;
                var labelW = box.Rect.width/2;
                if (isHorizontal)
                {
                    labelRect.width = labelW;
                    fieldRect.width -= labelW;
                    fieldRect.x += labelW;
                }

                GUI.Label(labelRect, label, XDStyles.Instance.Label);
            }

            return fieldRect;
        }

        public static Rect CreateEmptyPlaceHolder(int width, int height, bool isHorizontal = true)
        {
            var fieldRect = new Rect(0, 0, width, height);
            var style = new GUIStyle(GUIStyle.none);
            style.fixedWidth = width;
            style.fixedHeight = height;
            using (var box = new XDLayout(isHorizontal,style))
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

        public static void DebugLastRect()
        {
            var rect = GUILayoutUtility.GetLastRect();
            var debugColor = Color.red.Alpha(0.2F);
            EditorGUI.DrawRect(rect, debugColor);
        }
    }
}