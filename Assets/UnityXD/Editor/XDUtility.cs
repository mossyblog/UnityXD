using System;
using UnityEditor;
using UnityEngine;

namespace UnityXD.Editor
{
    public static class XDUtility
    {

        public static void CreateTextField(String label, ref string field, XDSizes fieldSize, bool isHorizontal = true)
        {
            var groupStyle = XDStyles.Instance.ResolveGroup(fieldSize, isHorizontal);
            using (var box = new XDLayout(isHorizontal, groupStyle))
            {
                GUILayout.Space(0);

                var labelRect = box.Rect;
                var fieldRect = box.Rect;

                if (isHorizontal)
                {
                    labelRect.width = (int) fieldSize;
                    fieldRect.width -= (int) fieldSize;
                    fieldRect.x += (int)fieldSize;
                }

                GUI.Label(labelRect, label, XDStyles.Instance.Label);
                field = GUI.TextField(fieldRect, field, XDStyles.Instance.Field);
            }
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

        public static void DebugLastRect()
        {
            var rect = GUILayoutUtility.GetLastRect();
            var debugColor = Color.red.Alpha(0.2F);
            EditorGUI.DrawRect(rect, debugColor);
        }

        public static Color Alpha(this Color clr, float amt)
        {
            clr.a = amt;
            return clr;
        }

    }
}