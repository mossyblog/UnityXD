using System;
using UnityEditor;
using UnityEngine;

namespace UnityXD.Editor
{
    public static class XDUtility
    {
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

        public static Rect CreateLabelPair(string label, XDSizes fieldSize, bool isHorizontal = true)
        {
            var groupStyle = XDStyles.Instance.ResolveGroup(fieldSize, isHorizontal);            
            var labelRect = new Rect(0,0,0,0);
            var fieldRect = new Rect(0, 0, 0, 0);

            
            using (var box = new XDLayout(isHorizontal, groupStyle))
            {
                GUILayout.Space(0);
                labelRect = box.Rect;
                fieldRect = box.Rect;
             
                if (isHorizontal)
                {
                    labelRect.width = (int)fieldSize * 2;
                    fieldRect.width -= (int)fieldSize * 2;
                    fieldRect.x += (int)fieldSize * 2;
                }

                GUI.Label(labelRect, label, XDStyles.Instance.Label);                
            }

            return fieldRect;
        }

        public static Texture2D CreateColoredTexture(Color col)
        {
            var width = 10;
            var height = 10;
            var pix = new Color[width * height];

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