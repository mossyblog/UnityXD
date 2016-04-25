using UnityEngine;
using System.Collections;
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
        }

        protected virtual void CommitProperties()
        {
            if (GUI.changed && _componentRef != null)
            {
                _componentRef.Text = m_text;
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
            XDUtility.CreateTextField("test", ref m_text, XDSizes.Large);
            XDUtility.DebugLastRect();
        }

        protected virtual void CreateDesignControls()
        {
        }

        protected virtual void CreateBindingControls()
        {
        }

      
    }
}
	
