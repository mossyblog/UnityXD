using UnityEditor;
using UnityEngine;
using System.Linq;

namespace UnityXD.XDGUIEditor
{
    

    public class XDGUIPanel : GUI.Scope
    {
        public XDGUIPanel(params GUILayoutOption[] options)
        {
            Rect = EditorGUILayout.BeginVertical(options);
        }

        public XDGUIPanel(bool isHorizontal, params GUILayoutOption[] options)
        {
            Rect = isHorizontal ? EditorGUILayout.BeginHorizontal(options) : EditorGUILayout.BeginVertical(options);
        }

        public XDGUIPanel(bool isHorizontal, GUIStyle style, params GUILayoutOption[] options)
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