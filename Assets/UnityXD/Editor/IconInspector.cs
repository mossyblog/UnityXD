using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Editor;
using UnityXD.Styles;
using UnityXD.XDGUIEditor;

namespace UnityXD.XDGUIEditor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Icon))]
    public class IconInspector : UIComponentInspector
    {
        protected Icon _iconRef;
        protected XDIcons m_currentIcon;

        protected override void Initialize()
        {
            if (_iconRef == null)
            {
                _iconRef = target as Icon;
            }

            base.Initialize();
        }

        protected override void CreateDesignControls()
        {
            base.CreateDesignControls();
            using (new XDGUIPanel(false, XDGUIStyles.Instance.Panel))
            {
                XDGUI.Create().Text("Icon").Size(64, 22, 48).ComboBox(ref m_currentIcon,  null, true);
            }
        }

        protected override void CommitProperties()

        {
            m_layout_paddingEnabled = false;
            XDGUIUtility.Bind(ref _iconRef.CurrentIcon, ref m_currentIcon);
            XDGUIUtility.Bind(ref _iconRef.CurrentStyle, ref m_style);
            base.CommitProperties();

            if (GUI.changed)
            {
                _iconRef.SetIcon(m_currentIcon);
            }

        }
    }
}
