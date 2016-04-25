using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityXD.Components;

namespace UnityXD.Editor
{
    [DisallowMultipleComponent]
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIComponent))]
    public class XDInspector : UnityEditor.Editor
    {
        protected UIComponent _componentRef;
        protected Label _label;
        protected string m_text;
        protected bool m_isVisible;

        public void OnEnable()
        {

        }

        public void OnDestroy()
        {
        }

        public override void OnInspectorGUI()
        {
            Initialize();
            GeneratateInspector();
            CommitProperties();
        }

        protected virtual void Initialize()
        {
            if (_componentRef == null)
                _componentRef = (UIComponent)target;

            m_text = _componentRef.Text;
            m_isVisible = _componentRef.IsVisible;
        }

        protected virtual void CommitProperties()
        {
            if (GUI.changed && _componentRef != null)
            {
                _componentRef.Text = m_text;
                _componentRef.IsVisible = m_isVisible;
            }
            EditorUtility.SetDirty(target);
        }

        protected virtual void GeneratateInspector()
        {
            CreateLayoutControls();
            CreateBindingControls();
            CreateDesignControls();
        }

        protected virtual void CreateLayoutControls()
        {
   
        }

        protected virtual void CreateDesignControls()
        {
        }

        protected virtual void CreateBindingControls()
        {
        }

      
    }
}
	
