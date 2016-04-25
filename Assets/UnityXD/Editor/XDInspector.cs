using System;
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
        protected const string m_labelLayout = "Layout";
        protected const string m_labelDesign = "Design";
        protected const string m_labelBinding = "Binding";

        protected string[] m_tabs = {m_labelLayout, m_labelDesign, m_labelBinding};
        protected string m_currentSelectedTab = m_labelLayout;

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
            CreateTabBar();

            if (m_currentSelectedTab == m_labelLayout)
                CreateLayoutControls();

            if (m_currentSelectedTab == m_labelDesign)
                CreateDesignControls();

            if (m_currentSelectedTab == m_labelBinding)
                CreateBindingControls();
            
        }

        protected virtual void CreateTabBar()
        {
            XDUtility.CreateTabBar(m_tabs, ref m_currentSelectedTab);

        }

        protected virtual void CreateLayoutControls()
        {
            GUILayout.Space(8);
            var icon = Resources.Load<Sprite>("icons/Editor/AlignHorizLeft_off");
            XDUtility.CreateButton(icon,32,32);
        }

        protected virtual void CreateDesignControls()
        {
        }

        protected virtual void CreateBindingControls()
        {
        }

      
    }
}
	
