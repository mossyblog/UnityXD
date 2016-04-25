using UnityEditor;
using UnityEngine;

namespace UnityXD.Editor
{
    public class XDLayout : GUI.Scope
    {
        public XDLayout(params GUILayoutOption[] options)
        {
            Rect = EditorGUILayout.BeginVertical(options);
        }

        public XDLayout(bool isHorizontal, params GUILayoutOption[] options)
        {
            Rect = isHorizontal ? EditorGUILayout.BeginHorizontal(options) : EditorGUILayout.BeginVertical(options);
        }

        public XDLayout(bool isHorizontal, GUIStyle style, params GUILayoutOption[] options)
        {
            Rect = isHorizontal ? EditorGUILayout.BeginHorizontal(style, options) : EditorGUILayout.BeginVertical(style, options);
        }

        protected override void CloseScope()
        {
            if (IsHorizontal)
            {
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.EndVertical();
            }
        }

        public bool IsHorizontal;

        public Rect Rect { get; protected set; }
    }
}